using dotnetcoremorningclass.Data;
using dotnetcoremorningclass.Models;
using dotnetcoremorningclass.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoremorningclass.Controllers
{
    public class AccountController : Controller
    {
        //htese are 2 dependencies from identity to perform dependency inversion with our constructor
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            //var response = new RegisterViewModel();
            if (!ModelState.IsValid) return View("Error");

            var user = await _userManager.FindByEmailAsync(registerVm.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email addresss is already in use";
                return View(registerVm);
            }

            var newUser = new AppUser
            {
                
                Email = registerVm.EmailAddress,
                UserName = registerVm.EmailAddress,
                EmailConfirmed = true,

            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVm.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            return RedirectToAction("Login", "Account");

        }


        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
           // var response = new LoginViewModel();
           //validation
           if (!ModelState.IsValid) return View(loginVm);
           // return View();


           var user = await _userManager.FindByEmailAsync(loginVm.EmailAddress);
           if (user != null)
           {
               //userpassword check
               var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVm.Password);
               if (passwordCheck)
               {
                   //password correct,then sign in
                   var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                   if (result.Succeeded)
                   {
                       return RedirectToAction("Index", "DashBoard");
                   }

               }

               TempData["Error"] = "wrong credential pls try again";
           }

           TempData["Error"] ="You are not a registered user, try again";

           return View(loginVm);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

    }
}
