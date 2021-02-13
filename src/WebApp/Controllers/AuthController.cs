using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI.Controllers {
    public class AuthController : Controller {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register(){
            return View();
        }
        
        [HttpPost]
        //[Authorize(Policy = "RequireAdministrator")]
        public async Task<IActionResult> Register(string email, string password) {
            IdentityUser user = new IdentityUser {
                Email = email,
                UserName = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) {
                await _signInManager.SignInAsync(user, false);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password) {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Loguot() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}