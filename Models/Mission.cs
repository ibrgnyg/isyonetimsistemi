using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Models
{
    public class Mission:BaseEntity
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public string Projectİcon  { get; set; }
        public bool SyncProgress{ get; set; }

        public virtual Category Category { get; set; }
        public int CategorryId { get; set; }
        public virtual AppUser  User { get; set; }
        public string userId { get; set; }

        public virtual ICollection<Chat> Chats { get; set; }
    }
}
