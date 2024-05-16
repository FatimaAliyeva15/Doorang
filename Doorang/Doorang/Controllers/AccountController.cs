using Doorang.ViewModel;
using Doorang_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Doorang.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser()
            {
               FullName = registerVM.FirstName,
               UserName = registerVM.UserName,
               Email = registerVM.Email

            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return RedirectToAction("Login");

        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        FullName = "Fatima Aliyeva",
        //        UserName = "FatimaAliyeva"
        //    };
        //    await _userManager.CreateAsync(admin, "Fatima123@");
        //    await _userManager.AddToRoleAsync(admin, "Admin");

        //    return Ok("Admin yarandi");

        //}

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole identityRole1 = new IdentityRole("Admin");
        //    IdentityRole identityRole2 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(identityRole1);
        //    await _roleManager.CreateAsync(identityRole2);

        //    return Ok("Rollar yarandi");
        //}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM adminLoginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(adminLoginVM.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "UserName or Password is not valid");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVM.Password, adminLoginVM.IsPersistent, false);

            if (result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not valid");
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
