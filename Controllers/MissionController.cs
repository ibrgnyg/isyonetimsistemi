using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ısyonetimsistemi.Data;
using ısyonetimsistemi.Models;
using ısyonetimsistemi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ısyonetimsistemi.Controllers
{
    public class MissionController : Controller
    {
        private readonly MissionRepository _missonRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly CategoryRespository _categoryRespository;
        private IHostingEnvironment _environment;
        private ApplicationDbContext _context;
        public MissionController(MissionRepository missionRepository,UserManager<AppUser> userManager,CategoryRespository  categoryRespository,IHostingEnvironment  environment,ApplicationDbContext context)
        {
            _context = context;
            _environment = environment;
            _categoryRespository = categoryRespository;
            _userManager = userManager;
            _missonRepository = missionRepository;
        }

        public IActionResult Index()
        {
            _context.Users.ToList();
            var missionlist = _missonRepository.GetAll();
            return View(missionlist);
        }

        [HttpGet]
        
        public IActionResult Create()
        {
            return View();

        }

        public async Task<IActionResult> Create(string _ProjectName,string _Description ,int _Progress ,string _Projectİcon , bool _SyncProgress, IFormFile upload, Mission mission)
        {
            if (upload != null)
            {

                var path = Path.Combine(_environment.WebRootPath, "İcon", upload.FileName);
                var stream = new FileStream(path, FileMode.Create);
                upload.CopyTo(stream);
               mission.Projectİcon = upload.FileName;
            }
            mission.ProjectName = _ProjectName;
            mission.Description = _Description;
            mission.Progress = _Progress;
            mission.SyncProgress = _SyncProgress;
            mission.CreatedDate = DateTime.Now;
            mission.CreatedBy = User.Identity.Name;
            mission.User = await _userManager.GetUserAsync(User);
            _missonRepository.save(mission);

            return RedirectToAction("Index", "Mission");
        }
        public IActionResult Delete(int id)
        {
            _missonRepository.Delete(_missonRepository.Find(id));

            return  RedirectToAction("Index", "Mission");
        }

        public IActionResult Detail(int id)
        {
           var Unknow = _missonRepository.Find(id);
            _context.chats.ToList();
            return View(Unknow);

        }
        [HttpGet]
        public IActionResult Edit (int id)
        {
            return View(_missonRepository.Find(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit (IFormFile upload, Mission mission)
        {
            if (upload != null)
            {

                var path = Path.Combine(_environment.WebRootPath, "İcon", upload.FileName);
                var stream = new FileStream(path, FileMode.Create);
                upload.CopyTo(stream);
                mission.Projectİcon = upload.FileName;
            }

            mission.UpdateDate = DateTime.Now;
            mission.CreatedBy = User.Identity.Name;
            mission.User = await _userManager.GetUserAsync(User);
            _missonRepository.Update(mission);

            return RedirectToAction("Index", "Mission");
        }
    }
}