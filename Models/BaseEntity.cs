using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Models
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
