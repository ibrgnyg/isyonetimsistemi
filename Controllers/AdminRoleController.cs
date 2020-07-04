using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ısyonetimsistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ısyonetimsistemi.Controllers
{
    public class AdminRoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public AdminRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole(Role model)
        {
            IdentityRole role = new IdentityRole { Name = model.RoleName };

            IdentityResult result = await _roleManager.CreateAsync(role);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }


        public IActionResult RoleList()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
    }
}