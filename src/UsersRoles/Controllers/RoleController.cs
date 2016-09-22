using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRoles.Models;
using UsersRoles.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace UsersRoles.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
