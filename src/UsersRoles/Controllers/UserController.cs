using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRoles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace UsersRoles.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController (ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var username = User.Identity.Name;
            var currentUser = _db.Users.SingleOrDefault(u => u.UserName == username);
            string fullName = currentUser.FirstName + " " + currentUser.LastName;
            ViewData.Add("FullName", fullName);

            return View();
        }
    }
}
