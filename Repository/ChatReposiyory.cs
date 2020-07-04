using ısyonetimsistemi.Data;
using ısyonetimsistemi.Interfaces;
using ısyonetimsistemi.Models;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Repository
{
    public class ChatReposiyory : IRepository<Chat>
    {
        private ApplicationDbContext _context;
        public ChatReposiyory(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(Chat entity)
        {

            if (entity != null)
            {
                _context.chats.Remove(entity);
                _context.SaveChanges();
            }
        }

        public Chat Find(int id)
        {
            return _context.chats.Find(id);
        }

        public IEnumerable<Chat> GetAll()
        {
            return _context.chats.ToList();
        }

        public void save(Chat entity)
        {
            if (entity != null)
            {
                _context.chats.Add(entity);
                _context.SaveChanges();

            }
        }

        public void Update(Chat entity)
        {
            if (entity != null)
            {
                Chat old = Find(entity.Id);
                _context.Entry(old).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
