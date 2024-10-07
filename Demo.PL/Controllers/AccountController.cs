using Demo.DataAccessLayer.Entities;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                var Result = await userManager.CreateAsync(User,model.Password);

                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);

                if (User is not null)
                {
                    var Result = await userManager.CheckPasswordAsync(User, model.Password);

                    if (Result)
                    {
                        var LoginResult = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (LoginResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password is InCorrect");
                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Email is InCorrect");
                }
            }
            return View(model);
        }

        #endregion

        #region SignOut

        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
