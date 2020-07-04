using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Interfaces
{
    public  interface IRepository<T>
    {
        void save(T entity);
        void Delete(T entity);
        void Update(T entity);
        T Find(int id);
        IEnumerable<T> GetAll();
    }
}
