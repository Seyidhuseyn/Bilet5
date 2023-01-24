using Bilet5.Models;
using Bilet5.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bilet5.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(registerVM.Username);
            if (user == null)
            {
                ModelState.AddModelError("Username", "Bu istifadeci artiq movcuddur");
                return View();
            }
            user = new AppUser 
            { 
                Email=registerVM.Email,
                FirstName=registerVM.Name, 
                LastName=registerVM.Surname,
                UserName = registerVM.Username,
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
