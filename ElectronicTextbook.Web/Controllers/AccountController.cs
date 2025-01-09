using ElectronicTextbook.Core.Enums;
using ElectronicTextbook.Core.Interfaces;
using ElectronicTextbook.Core.Models;
using ElectronicTextbook.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElectronicTextbook.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }


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
                var role = new Role { Name = RoleType.Student.ToString() };
                User user = new User { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Role = role };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var foundUser = await _userManager.FindByEmailAsync(user.Email);

                    if (foundUser != null)
                    {
                        var userIdClaim = new System.Security.Claims.Claim("UserId", foundUser.Id.ToString());
                        await _userManager.AddClaimAsync(foundUser, userIdClaim);
                        await _signInManager.SignInAsync(foundUser, isPersistent: false);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var userIdClaim = new System.Security.Claims.Claim("UserId", user.Id.ToString());
                    if (user != null)
                    {
                        await _userManager.AddClaimAsync(user, userIdClaim);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}