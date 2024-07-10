using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewModels;
using Shortly.Data;
using Shortly.Data.Services;

namespace Shortly.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUsersService _userService;
        public AuthenticationController(IUsersService userService) 
        {
            _userService = userService;
        }
        public async Task<IActionResult> Users()
        {
            var user = await _userService.GetUsersAsync();
            return View(user);
        }
        public async Task<IActionResult> Login()
        {
            return View(new LoginVM());
        }
        public async Task<IActionResult> LoginSubmitted(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View("Login",loginVM);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Register()
        {
            return View(new RegisterVM());
        }
        public async Task<IActionResult> RegisterUser(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View("Register", registerVM);
            return RedirectToAction("Index", "Home");
        }
    }
}
