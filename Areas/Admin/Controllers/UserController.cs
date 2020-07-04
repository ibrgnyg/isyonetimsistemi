using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ısyonetimsistemi.Data;
using ısyonetimsistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ısyonetimsistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManger;
        public UserController(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _userManger = userManager;
            _context = context;
        }
        public IActionResult Index(int order)
        {
            ViewBag.Order = new List<SelectListItem>()
            {
                new SelectListItem {Text ="A dan Z Listele " ,Value ="1", Selected =true},
                 new SelectListItem{ Text ="Z den A Listele", Value ="2", },
                 new SelectListItem{Text= "En Çok Bulunan", Value="3"},
                 new SelectListItem{Text= "En Çok Makale Bulununan", Value="4"},
            };

            var users = _context.Users.ToList();
            var result = _context.Users.ToList();
            if (order == 1)
            {
                result = result = _context.Users.OrderBy(x => x.UserName).ToList();
            }
            else if (order == 2)
            {
                result = result = _context.Users.OrderByDescending(a => a.UserName).ToList();
            }
           

   
            return View(result);
        }

        public async Task<IActionResult> userDetail(string id)
        {
            if(id== null)
            {
                return NotFound();
            }
            //burda id ile kullanıcyı buluyoruz
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