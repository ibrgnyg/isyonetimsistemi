using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ısyonetimsistemi.Models;
using ısyonetimsistemi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ısyonetimsistemi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRespository _categoryRespository;
        public CategoryController(CategoryRespository categoryRespository)
        {
            _categoryRespository = categoryRespository;
        }

        public IActionResult Index()
        {
            return View(_categoryRespository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(string _Name, string _Color)
        {

            var category = new Category()
            {
                Name = _Name,
                Color = _Color,
                CreatedDate = DateTime.Now,
            };
            _categoryRespository.save(category);

            return RedirectToAction("Index","Category");
        }


        public IActionResult Delete(int id)
        {
            _categoryRespository.Delete(_categoryRespository.Find(id));

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Detail(int id)
        {
           var details= _categoryRespository.Find(id);
            return View(details);

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
           
            return View(_categoryRespository.Find(id));

        }

        [HttpPost]
        public IActionResult Update(Category category)
        {

            category.UpdateDate = DateTime.Now;
            _categoryRespository.Update(category);
            return RedirectToAction("Index", "category");
            

        }
    }
}