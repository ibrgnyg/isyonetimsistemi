using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ısyonetimsistemi.Models;
using ısyonetimsistemi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ısyonetimsistemi.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatReposiyory _chatReposiyory;
        public ChatController(ChatReposiyory chatReposiyory)
        {
            _chatReposiyory = chatReposiyory;
        }


        public IActionResult AddMessage (string _Description, int _ID)
        {
            var chat = new Chat()
            {
               TaskId =_ID,
                Description = _Description,
                CreatedDate = DateTime.Now,

            };
            _chatReposiyory.save(chat);
            return Ok();
        }

        public IActionResult Delete(int id)
        {
            _chatReposiyory.Delete(_chatReposiyory.Find(id));
            return RedirectToAction("Index", "Details");
        }
    }
}