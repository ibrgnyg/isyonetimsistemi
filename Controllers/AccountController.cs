using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ısyonetimsistemi.Data;
using ısyonetimsistemi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ısyonetimsistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManager;
        private IHostingEnvironment _environment;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager,IHostingEnvironment hostingEnvironment,ApplicationDbContext context)
        {
            _context = context;
            _environment = hostingEnvironment;
            _signInManager = signInManager;
            _userManger = userManager;
        }
        
        
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //burada post işlemi yapacağımız belirtiyoruz
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            var user = new AppUser()
            {
                UserName = register.Email,
                Email = register.Email,

            };
            //uye olma tarihini tutuyoruz
            user.RegisterDate = DateTime.Now;
            //yukarıda oluşturgumuz user değişkenşini burada kullanıp usermanager sınıfındaku create methudu ile kullanıcı kaydediyoruz
            var result = await _userManger.CreateAsync(user, register.Password);
            //uye olma başarılı ise  kullanıcıya bilgilendirici bir mail mesajı gonderiyoruz
            if (result.Succeeded)
            {
                //burda uye olan kullanıcının email adresni alıp mail gonderiyoruz 
                var email = _userManger.FindByEmailAsync(register.Email);

                MailMessage mail = new MailMessage
                {
                    IsBodyHtml = true
                };
                mail.To.Add(user.Email);
                mail.From = new MailAddress("ısyonetimsitesmi@gmail.com", "Uye Olma İşlemi Başarılı", System.Text.Encoding.UTF8);
                mail.Subject = "Üye Olma İşlemi Başarılı";

                //burda kullanıcay wwwroot altında bir html dosya 
                var pathToFile = _environment.WebRootPath
                       + Path.DirectorySeparatorChar.ToString()
                       + "Gmail"
                       + Path.DirectorySeparatorChar.ToString()
                       + "Register.html";

                using (StreamReader stream = System.IO.File.OpenText(pathToFile))
                {
                    pathToFile = stream.ReadToEnd();
                }

                mail.Body = pathToFile;
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient
                {
                    Credentials = new NetworkCredential("hangi gmaile uzerinde gondericeksek o gmaile adresi yazılacak", " ve bir de şifre"),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Timeout = 10000
                };
                smp.Send(mail);


                //eğe uye olma başarılı ise burda kullanıcıya ekranda maile gitti mesajını gosteriyoruz
                string mes = $"Üye Olma işlemi başarılı {register.Email}  Gönderilmiştir.";
                //viewvag ile hata mesajlarını view kısmına taşıyorum
                ViewBag.Message1 = mes;

            }

            else
            {
                //eğer kullanıcı tekar aynı gmaile ile kayıt açmayı denerese burda o gmaile ait bir kayıt var mesajı gosteriyoruz 
                string mes = $"{register.Email} Böyle Bir Kullanıcı Mevcut ";
                ViewBag.Message2 = mes;
            }

            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if(ModelState.IsValid)
            {
                //burda kullanıcıyı kontrol ediyoru varmı yokmu 
                var user = await _userManger.FindByEmailAsync(login.Email);
                //eğer varsa 
                if (user != null)
                {
                    //yukarıdan gelen değişkenle giriş yapma tarihini tutuyoruz
                    user.LoginDate = DateTime.Now;
                    //kullanıcını girdiği şifreyi kontol ediyoruz
                    var lgin = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                    //kullanıcıyı burda update ediyoruz ki giriş yapma tarihi vertabanına yansısın
                    await _userManger.UpdateSecurityStampAsync(user);
                    //eger giriş başarılı ise ana sayfaya yonelendiriyoruz
                    if (lgin.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        //hesap kitlendiyse başka bir action a yenlendiriyoruz 
                        await _userManger.GetLockoutEnabledAsync(user);
                        if (lgin.IsLockedOut)
                        {
                            return RedirectToAction("Lockout", "Account");
                        }

                        //burda hata mesajlarını yazdırıyoruz
                        string mes = "E-posta veya Şifre hatalı!";
                        ViewBag.Message = mes;

                    }

                }
                else
                {

                    string noUser = $"{login.Email} Böyle bir kullanıcı Bulunmamaktadır.!";
                    ViewBag.NotUser = noUser;
                }
            }
            return View();
           
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
       

    }
    
}