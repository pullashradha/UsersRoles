﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRoles.Models;
using UsersRoles.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UsersRoles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Register New Account
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var newUser = new User { UserName = model.Username };
            IdentityResult registeredUser = await _userManager.CreateAsync(newUser, model.Password);
            if (registeredUser.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Log In User
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Log Out User
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
