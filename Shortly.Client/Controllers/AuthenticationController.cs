using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewModels;
using Shortly.Client.Helpers.Roles;
using Shortly.Data;
using Shortly.Data.Models;
using Shortly.Data.Services;

namespace Shortly.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUsersService _userService;
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        public AuthenticationController(IUsersService userService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) 
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
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

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                var userPasswordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (userPasswordCheck)
                {
                    var userLoggedIn = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (userLoggedIn.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userLoggedIn.IsNotAllowed)
                    {
                        
                        return RedirectToAction("EmailConfirmation");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt. Please check your username and password, and try again.");
                        return View("Login", loginVM);
                    }
                }
                else
                {
                    await _userManager.AccessFailedAsync(user);

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Your account is locked, please try again in 10 minutes.");
                        return View("Login", loginVM);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt. Please check your username and password, and try again.");
                        return View("Login", loginVM);
                    }
                }
            }

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

            // Check if user exist - before registering the user
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                ModelState.AddModelError("", "User already have an account.");
                return View("Register", registerVM);
            }

            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                FullName = registerVM.FullName,
                LockoutEnabled = true
            };

            var userCreated = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (userCreated.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, Role.User);
                await _signInManager.PasswordSignInAsync(newUser, registerVM.Password, false, false);

            }
            else
            {
                foreach(var error in userCreated.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Register", registerVM);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EmailConfirmation()
        {
            var confirmEmail = new ConfirmLoginVM();
            return View(confirmEmail);
        }
    }
}
