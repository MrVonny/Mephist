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
using Mephist.Services.DAL;
using Mephist.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Mephist.Controllers
{
    public partial class EducationalMaterialsController : Controller
    {

        private readonly UnitOfWork universityData;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UniversityStaticData _universityStaticData;

        readonly List<SelectListItem> Types;

        public EducationalMaterialsController(UnitOfWork universityData, IWebHostEnvironment webHost, UserManager<User> userManager, SignInManager<User> signInManager, UniversityStaticData universityStaticData)
        {
            this.universityData = universityData;
            _webHost = webHost;
            _userManager = userManager;
            _signInManager = signInManager;
            _universityStaticData = universityStaticData;
            Types = new List<SelectListItem>()
            {
                new SelectListItem("Другое","-1"),
                new SelectListItem("Лекции","1"),
                new SelectListItem("ДЗ","2"),
                new SelectListItem("Шпоры","3"),
                new SelectListItem("Лабараторная работа","4"),
                new SelectListItem("Билеты","5"),
                new SelectListItem("Курсовая работа","6")
            };
        }

        public async Task<IActionResult> Download(int id)
        {
            EducationalMaterial em = await universityData.EducationalMaterials.GetByIdAsync(id);
            List<Media> materials = em.Materials;
            List<byte[]> files = new List<byte[]>();
            byte[] compressed;

            foreach (var material in materials)
            {
                string path = _webHost.WebRootPath;
                files.Add(System.IO.File.ReadAllBytes(Path.Combine(path, material.GetPath())));

            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);
                for (int i = 0; i < materials.Count; i++)
                {
                    var fileInArchive = archive.CreateEntry(materials[i].MediaName, CompressionLevel.Optimal);

                    using var entryStream = fileInArchive.Open();
                    using var fileToCompressStream = new MemoryStream(files[i]);
                    fileToCompressStream.CopyTo(entryStream);

                }
                archive.Dispose();

                compressed = memoryStream.ToArray();


            }
            //GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced, false, true);
            
            return File(compressed, "application/zip", em.Name + ".zip");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddMaterial()
        {
            ViewBag.Types = Types;
            return View();

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return StatusCode(400);

            LaboratoryJournal lj = await universityData.LaboratoryJournals.GetByIdAsync(id.Value);

            return View(lj);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        //ToDo:
        //Убрать returnUrl и сделать удаление через AJAX.
        public async Task<IActionResult> Delete(int? id, string returnUrl)
        {
            if (id is null)
                return StatusCode(400);
            universityData.EducationalMaterials.Remove(await universityData.EducationalMaterials.GetByIdAsync(id.Value));
            universityData.SaveAsync();
            return RedirectToAction("GetMaterials");
        }


        [HttpGet]
        public JsonResult SubjectsAutocompleteSearch()
        {
            var subjects = _universityStaticData.GetSubjects();             
            return Json(subjects.Select(x=> new { value = x}));
        }
    }
}
