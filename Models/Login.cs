using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ısyonetimsistemi.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Lütfen bir  E-posta giriniz.!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen uygun formatta E-posta giriniz.!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen bir Şifre giriniz.!")]
        public string Password { get; set; }
    }
}
