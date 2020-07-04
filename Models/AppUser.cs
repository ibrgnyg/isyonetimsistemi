using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public DateTime RegisterDate { get; set; }
        public  DateTime LoginDate { get; set; }
        public virtual ICollection<Mission> Missions { get; set; }
    }
}
