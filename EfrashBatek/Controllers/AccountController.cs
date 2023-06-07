using EfrashBatek.Models;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System;


namespace EfrashBatek.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.Phone,
                age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.Birthdate,
                Gender = (Gender)model.Gender,

            };
           

            var result = await _userManager.CreateAsync(user, model.Password);//Created Cookies

            if (result.Succeeded)
            {

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var fromAddress = new MailAddress("omran942487@gmail.com", "Mohamed");
                var toAddress = new MailAddress(user.Email, user.UserName);
                const string subject = "Confirm your account";
                string body = $"Please confirm your account by clicking this link: <a href='{Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme)}'>link</a>";

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("omran942487@gmail.com", "Mohamed890@#")
                };
                //To send Message
                using (var messages = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    await smtpClient.SendMailAsync(messages);
                }
                return View("EmailConfirmation");
            }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            return View(model);
            /***********/
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "~/Home/TrendingProducts")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "~/Home/TrendingProducts")
        {
            ViewData["ReturnUrl"] = returnUrl;
           
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {//ispersistanc to detct user is session or cookies
                    SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: model.isPersistent, false);

                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("Id", user.Id);
                        return RedirectToLocal(returnUrl);
                    }
                    else
						ModelState.AddModelError("", "Invalid Email Or Password.");
				}
                else
                    ModelState.AddModelError("", "Invalid Email Or Password.");
                return View(model);
            }
       
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmPassword);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Login", "Account");

                }
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", er.Description);
                }

            }
            return View(model);

        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var conf = await _userManager.IsEmailConfirmedAsync(user);
                if (user == null || !conf)
                {
                    ModelState.AddModelError("", "The Email Not Found");
                    return View();
                }
                var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callback = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //    $"Please reset your password by clicking here: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");
                ////model.Result = "A password reset link has been sent to your email.";
                //var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //         $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction("ResetPassword", model.Email, Token);
            }
            return View(model);
        }
        public IActionResult ResetPassword(string Email, string token)
        {
            ResetPasswordVM model = new ResetPasswordVM();
            model.Email = Email;
            model.Token = token;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email Not Found ^^");
                    return View(model);
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("TrendingProducts", "Home");
                }
                else
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                return View(model);
            }
            return View(model);
        }



        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.TrendingProducts), "Home");
            }
        }
    }
}