using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime;
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

        
        public IActionResult Download(int id)
        {
            EducationalMaterial em = _repository.GetEducationalMaterial(id);
            List<Media> materials = em.Materials;
            List<byte[]> files = new List<byte[]>();
            byte[] compressed;

            foreach (var material in materials)
            {
                string path = _webHost.WebRootPath;
                files.Add(System.IO.File.ReadAllBytes(Path.Combine(path, material.GetPath())));

            }

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    for (int i = 0; i < materials.Count; i++)
                    {
                        var fileInArchive = archive.CreateEntry(materials[i].MediaName, CompressionLevel.Optimal);

                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(files[i]))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                        }
                        
                    }
                    archive.Dispose();

                    compressed = memoryStream.ToArray();
                }

                
            }
            //GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, false, true);
            
            return File(compressed, "application/zip", em.Name + ".zip");
        }

        [Authorize]
        public IActionResult AddMaterial()
        {
            
            ViewBag.Types = Types;
            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddLabJournal(EducationalMaterialViewModel model, string query, IFormFileCollection uploads)
        {
            List<Media> medias = new List<Media>();
            try
            {
                Employee employee = _repository.GetEmployee(model.EmployeeFullName);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("FullName", ex.Message);
            }

            if (model.Work == null)
                ModelState.AddModelError("Work", "Введите название работы");
            if (model.Year == null)
                ModelState.AddModelError("Year", "Введите год выполнения работы");
            if (model.Semester == null)
                ModelState.AddModelError("Semester", "Введите семестр выполнения работы");
            if (model.Mark == null)
                ModelState.AddModelError("Mark", "Ввеодите оценку за работу");

            if (uploads.Count <= 0)
                ModelState.AddModelError("Files", "Не загржен ни один файл");

            if (ModelState.IsValid)
            {
                LaboratoryJournal lj = new LaboratoryJournal()
                {
                    Name = model.Name,
                    Employee = _repository.GetEmployee(model.EmployeeFullName),
                    Description = model.Description,
                    Subject = model.Subject,
                    Type = model.Type,
                    Materials = medias,

                    Mark = model.Mark ?? throw new ArgumentNullException(),
                    Semester = model.Semester ?? throw new ArgumentNullException(),
                    Work = model.Work ?? throw new ArgumentNullException(),
                    Year = model.Year ?? throw new ArgumentNullException()
                };
                _repository.CreateLaboratoryJournal(lj);

                string patrialPath = "Content/EducationalMaterials/" + String.Format("{0}_{1}",
                    DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"), model.Name.Transliterate());
                string path = Path.Combine(_webHost.WebRootPath, patrialPath);
                Directory.CreateDirectory(path);
                for (int i = 0; i < uploads.Count; i++)
                {
                    var file = uploads[i];
                    string fileName= (i + 1).ToString()+"."+file.FileName.Split('.').Last();
                    using (var fileSteam = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileSteam);
                    }
                    medias.Add(new Media(lj,
                    fileName, file.ContentType, patrialPath, await _userManager.GetUserAsync(User)));
                }

                foreach (var media in medias)
                    media.EducationalMaterial = lj;
                _repository.CreateMediaRange(medias);
                _repository.SaveChanges();

                return RedirectToAction("GetMaterials", "EducationalMaterials");
            }
            ViewBag.Types = Types;
            return View("AddMaterial", model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMaterial(EducationalMaterialViewModel model, IFormFileCollection uploads)
        {
            try
            {
                Employee employee = _repository.GetEmployee(model.EmployeeFullName);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("FullName", ex.Message);

            }
            if (uploads.Count <= 0) ModelState.AddModelError("Files", "Не загржен ни один файл");

            List<Media> medias = new List<Media>();


            if (ModelState.IsValid)
            {


                EducationalMaterial em = new EducationalMaterial()
                {
                    Name = model.Name,
                    Employee = _repository.GetEmployee(model.EmployeeFullName),
                    Description = model.Description,
                    Subject = model.Subject,
                    Type = model.Type,
                    Materials = medias
                };
                _repository.CreateEducationalMaterial(em);


                string patrialPath = "Content/EducationalMaterials/" + String.Format("{0}_{1}",
                    DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"), model.Name.Transliterate());
                string path = Path.Combine(_webHost.WebRootPath, patrialPath);
                Directory.CreateDirectory(path);
                foreach (var file in uploads)
                {

                    using (var fileSteam = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileSteam);

                    }

                    medias.Add(new Media(em,
                    file.FileName, file.ContentType, patrialPath, await _userManager.GetUserAsync(User)));

                }

                foreach (var media in medias)
                    media.EducationalMaterial = em;
                _repository.CreateMediaRange(medias);
                _repository.SaveChanges();

                return RedirectToAction("GetMaterials", "EducationalMaterials");
            }
            ViewBag.Types = Types;
            return View(model);
        }
    }
}
