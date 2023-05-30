using EfrashBatek.Models;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
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
                // Add user to default role
              //  await _userManager.AddToRoleAsync(user, "User");

                // Redirect the user to the login page
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl ="~/Home/Index")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "~/Home/Index")
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
            User user=await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(user,model.Password,model.isPersistent,false);

                    if (result.Succeeded)
                    {
                        return RedirectToLocal (returnUrl);
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
