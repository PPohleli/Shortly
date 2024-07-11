﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewModels;
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
            return RedirectToAction("Index", "Home");
        }
    }
}
