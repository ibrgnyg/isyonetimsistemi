using ısyonetimsistemi.Data;
using ısyonetimsistemi.Interfaces;
using ısyonetimsistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Repository
{
    public class CategoryRespository : IRepository<Category>
    {
        private ApplicationDbContext _context;
        public CategoryRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(Category entity)
        {
            if(entity!=null)
            {
                _context.categories.Remove(entity);
                _context.SaveChanges();
                    
            }
        }

        public Category Find(int id)
        {
            return _context.categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.categories.ToList();
        }

        public void save(Category entity)
        {
            if (entity != null)
            {
                _context.categories.Add(entity);
                _context.SaveChanges();

            }
        }

        public void Update(Category entity)
        {
            if (entity != null)
            {
                Category old = Find(entity.Id);
                _context.Entry(old).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
