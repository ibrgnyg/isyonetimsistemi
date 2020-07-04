using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ısyonetimsistemi.Data;
using ısyonetimsistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ısyonetimsistemi.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManger;
        public UserController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManger = userManager;
            _context = context;
        }
        public IActionResult Index( )
        {

            var result = _context.Users.ToList();
            return View(result);
        }

        public async Task<IActionResult> userdetail(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManger.FindByIdAsync(id);
            return View(user);


        }

        public async Task<IActionResult> UserDelete(string id)
        {
            var user = await _userManger.FindByIdAsync(id);
            await _userManger.DeleteAsync(user);
            return RedirectToAction("Index", "User");
        }
    }
}