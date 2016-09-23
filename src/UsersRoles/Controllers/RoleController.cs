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
            var rolesList = _db.Roles.ToList();

            var userRolesList = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = userRolesList;

            var usersByRole = _db.UserRoles;
            ViewBag.UserRoles = usersByRole;

            Console.WriteLine(usersByRole);

            return View(rolesList);
        }

        //Create a New Role
        [HttpPost]
        public async Task<IActionResult> Create(string newName)
        {
            var newRole = new IdentityRole();
            newRole.Name = newName;
            IdentityResult result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Add User to Role
        [HttpPost]
        public async Task<IActionResult> AddUser(string username, string roleName)
        {
            User user = _db.Users.Where(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            IdentityResult result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                return View();
            }
        }
    }
}
