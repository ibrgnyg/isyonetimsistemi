using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Models
{
    public class Category:BaseEntity
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public  virtual ICollection<Mission> Tasks { get; set; }
    }
}
