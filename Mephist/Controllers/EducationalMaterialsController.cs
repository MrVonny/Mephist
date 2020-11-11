using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using Mephist.Services;
using Mephist.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mephist.Controllers
{
    public class EducationalMaterialsController : Controller
    {

        private IUniversityRepository _repository;
        private IWebHostEnvironment _webHost;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        List<SelectListItem> Types = new List<SelectListItem>()
            {
                new SelectListItem("Другое","-1"),
                new SelectListItem("Лекции","1"),
                new SelectListItem("ДЗ","2"),
                new SelectListItem("Шпоры","3"),
                new SelectListItem("Лабараторная работа","4"),
                new SelectListItem("Билеты","5"),
                new SelectListItem("Курсовая работа","6")
            };

        public EducationalMaterialsController(IUniversityRepository repository, IWebHostEnvironment webHost, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _repository = repository;
            _webHost = webHost;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult GetMaterials(EducationMaterialType[] types, string[] employees)
        {
            IEnumerable<EducationalMaterial> educationalMaterials = _repository.GetEducationalMaterials();
            if (types.Length>0)
                educationalMaterials=educationalMaterials.Where(em => types.Contains(em.Type));
            if (employees.Length>0)
                educationalMaterials=educationalMaterials.Where(em => employees.Contains(em.Employee.FullName));
            List<EducationalMaterial> list = educationalMaterials.ToList();
            ViewBag.EducationalMaterialsList = list;
            return View();
        }

        [Authorize]
        public IActionResult AddMaterial()
        {
            
            ViewBag.Types = Types;
            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMaterial(EducationalMaterialViewModel model, IFormFileCollection uploads)
        {
            if (model.Type == EducationMaterialType.LaboratoryJournal)
            {
                if (model.Work == null)
                    ModelState.AddModelError("Work", "Введите название работы");
                if (model.Year == null)
                    ModelState.AddModelError("Year", "Введите год выполнения работы");
                if (model.Semester == null)
                    ModelState.AddModelError("Semester", "Введите семестр выполнения работы");
                if (model.Mark == null)
                    ModelState.AddModelError("Mark", "Ввеодите оценку за работу");
            }
            List<Media> medias = new List<Media>();
            if (uploads.Count>0)
            {
                Employee employee=null;
                try
                {
                    employee = _repository.GetEmployee(model.EmploeeFullName);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("FullName", ex.Message);
                    
                }
                
                foreach (var file in uploads)
                {
                    string patrialPath = "EducationalMaterials/" + model.Name.Transliterate();
                    string path = Path.Combine(_webHost.WebRootPath, patrialPath);
                    
                    using(var fileSteam = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(fileSteam);
                    }

                    medias.Add(new Media(null,employee,
                        file.FileName,patrialPath,await _userManager.GetUserAsync(null)));
                }
                
            }
            else
                ModelState.AddModelError("Files", "Не загржен ни один файл");
            
            if(ModelState.IsValid)
            {
                
                if(model.Type==EducationMaterialType.LaboratoryJournal)
                {
                    LaboratoryJournal lj = new LaboratoryJournal()
                    {
                        Name = model.Name,
                        Employee = _repository.GetEmployee(model.EmploeeFullName),                    
                        Description=model.Description,
                        Subject=model.Subject,
                        Type=model.Type,
                        Materials=medias,

                        Mark = model.Mark ?? throw new Exception("Wrong model"),
                        Semester = model.Semester ?? throw new Exception("Wrong model"),
                        Work = model.Work,
                        Year = model.Year ?? throw new Exception("Wrong model")
                    };
                    _repository.CreateLaboratoryJournal(lj);
                    foreach (var media in medias)
                        media.EducationalMaterial = lj;
                    _repository.CreateMediaRange(medias);
                    _repository.SaveChanges();
                }
                else
                {
                    EducationalMaterial em = new EducationalMaterial()
                    {
                        Name = model.Name,
                        Employee = _repository.GetEmployee(model.EmploeeFullName),
                        Description = model.Description,
                        Subject = model.Subject,
                        Type = model.Type,
                        Materials = medias
                    };
                    _repository.CreateEducationalMaterial(em);
                    foreach (var media in medias)
                        media.EducationalMaterial = em;                 
                    _repository.CreateMediaRange(medias);
                    _repository.SaveChanges();
                }

                return RedirectToAction("GetMaterials", "EducationalMaterials");
            }
            ViewBag.Types = Types;
            return View(model);
        }
    }
}
