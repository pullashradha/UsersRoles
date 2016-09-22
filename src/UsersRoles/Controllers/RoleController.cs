using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRoles.Models;
using UsersRoles.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace UsersRoles.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Create a New Role
        [HttpPost]
        public async Task<IActionResult> NewRole(string newName)
        {
            var newRole = new IdentityRole ();
            newRole.Name = newName;
            IdentityResult result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                return Json(newRole);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
