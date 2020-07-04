using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ısyonetimsistemi.Models;
using ısyonetimsistemi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ısyonetimsistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRespository _categoryRespository;
        public CategoryController(CategoryRespository  categoryRespository)
        {
            _categoryRespository = categoryRespository;
        }
        public IActionResult Index()
        {
            return View(_categoryRespository.GetAll());
        }

        [HttpGet]
        public  IActionResult CreatePartial()
        {
            return View();
        }

        public IActionResult CreatePartial(string _Name,string _Color)
        {

            var category = new Category()
            {
                Name = _Name,
                Color = _Color,
            };
            _categoryRespository.save(category);
            return View();

        }
    }
}