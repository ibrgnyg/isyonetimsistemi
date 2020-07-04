using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Models
{
    public class Chat:BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual Mission Task { get; set; }
        public int TaskId { get; set; }


    }
}
