using ısyonetimsistemi.Data;
using ısyonetimsistemi.Interfaces;
using ısyonetimsistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Repository
{
    public class MissionRepository : IRepository<Mission>
    {
        private ApplicationDbContext _context;
        public MissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(Mission entity)
        {
         
            if(entity!= null)
            {
                _context.missions.Remove(entity);
                _context.SaveChanges();
            }
        }

        public Mission Find(int id)
        {
            return _context.missions.Find(id);
        }

        public IEnumerable<Mission> GetAll()
        {
            return _context.missions.ToList();
        }

        public void save(Mission entity)
        {
            if (entity != null)
            {
                _context.missions.Add(entity);
                _context.SaveChanges();

            }
        }

        public void Update(Mission entity)
        {
            if (entity != null)
            {
                Mission old = Find(entity.Id);
                _context.Entry(old).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
