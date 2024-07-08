using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewModels;
using Shortly.Data;

namespace Shortly.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private AppDbContext _appDbContext;
        public AuthenticationController(AppDbContext appContext) 
        {
            _appDbContext = appContext;
        }
        public IActionResult Users()
        {
            var user = _appDbContext.Users.Include(n => n.Urls).ToList();
            return View(user);
        }
        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        public IActionResult LoginSubmitted(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View("Login",loginVM);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }
        public IActionResult RegisterUser(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View("Register", registerVM);
            return RedirectToAction("Index", "Home");
        }
    }
}
