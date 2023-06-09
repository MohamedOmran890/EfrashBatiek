using Castle.MicroKernel.Registration;
using EfrashBatek.Models;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System;
using EfrashBatek.service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;

namespace EfrashBatek.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailService _emailService;
        private readonly IIdentityRepository _identityRepository;
       
        private readonly Context context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            EmailService emailService, IIdentityRepository identityRepository ,Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _identityRepository = identityRepository;
   
            this.context = context;
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
        public IActionResult Register2()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            var email=context.Users.FirstOrDefault(x=>x.Email==model.Email);
            if(email!=null)
            {
                ModelState.AddModelError("", "The Email is already Found");
                return View(model);
            }
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
            Customer customer = new Customer();
            customer.UserId = user.Id;
            
       
            context.Customers.Add(customer);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                {
                //
                var roleName = "Customer";
                await _userManager.AddToRoleAsync(user, roleName);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                  await _emailService.SendConfirmationEmail(model.Email, confirmationLink);
                return View("ConfirmEmail");
            }

          
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            return View(model);
               }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("TrendingProducts", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                
                return RedirectToAction("TrendingProducts","Home");
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
                if(user!=null&&!user.EmailConfirmed)
                {
                    //return View("PleaseConfirmYouEmail");
                    ModelState.AddModelError("", "Email Not Confirmed .");
                    return View(model);
                }
                
                if (user != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: model.isPersistent, false);

                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("Id", user.Id);
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Seller"))
                        {
                            return RedirectToAction("Seller", "ItemShop");
                        }
                        else if(await _userManager.IsInRoleAsync(user,"Customer"))
                        {
                            return RedirectToAction("TrendingProducts","Home");
                        }
                        else if(await _userManager.IsInRoleAsync(user,"Shop"))
                        {
                            return RedirectToAction("Seller", "ItemShop");
                        }
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
        
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                //var conf=await _userManager.IsEmailConfirmedAsync(user);
                if (user == null)
                {
                    ModelState.AddModelError("", "The Email Not Found");
                    return View();
                }
                //
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ResetPassword", "Account", new { Email = user.Email,Token = token, clicked = true }, Request.Scheme);
                await _emailService.SendForgetPassword(model.Email, confirmationLink);
                return RedirectToAction("ResetPassword", "Account", new { Email = model.Email, Token = token });
            }
            return View(model);
        }
        public IActionResult ResetPassword(string Email, string Token,bool clicked=false)
        {
            if(!clicked)
            {
               
                    return View("Clicked");
                
            }
            if (string.IsNullOrEmpty(Token))
            {
                ViewBag.Message = "Please check your email and click on the link to reset your password.";
                return View("Login");
            }
            var model = new ResetPasswordVM
            {
                Email = Email,
                Token = Token
            };
            return View(model);

            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (model == null)
            {
                return BadRequest("Invalid model");
            }
         
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email Not Found");
                    return View(model);
                }
                if (!user.EmailConfirmed)
                {
                    ViewBag.Message = "Please check your email and click on the link to reset your password.";
                    return View("Error"); 
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("TrendingProducts", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
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
