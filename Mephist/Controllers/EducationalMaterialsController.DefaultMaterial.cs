using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime;
using System.Threading.Tasks;
using FuzzySharp;
using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using Mephist.Models.Enums;
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
    public partial class EducationalMaterialsController : Controller
    {
        public IActionResult GetMaterials(string name, string employee, string lectures, string homeworks, string cheatsSheats, string labs, string tickets, string courseworks, string others)
        {
            List<EducationalMaterialType> types = new List<EducationalMaterialType>();
            
            if (!String.IsNullOrEmpty(lectures))
                types.Add(EducationalMaterialType.Lectures);
            if (!String.IsNullOrEmpty(homeworks))
                types.Add(EducationalMaterialType.Homework);
            if (!String.IsNullOrEmpty(cheatsSheats))
                types.Add(EducationalMaterialType.CheatSheets);
            if (!String.IsNullOrEmpty(labs))
                types.Add(EducationalMaterialType.LaboratoryJournal);
            if (!String.IsNullOrEmpty(tickets))
                types.Add(EducationalMaterialType.ExamTickets);
            if (!String.IsNullOrEmpty(courseworks))
                types.Add(EducationalMaterialType.TermPaper);
            if (!String.IsNullOrEmpty(others))
                types.Add(EducationalMaterialType.Other);
            

            IQueryable<EducationalMaterial> list = _repository.GetEducationalMaterials().AsQueryable();
            if(types.Count>0)
                list = list.Where(em => types.Contains(em.Type));
            if (!String.IsNullOrEmpty(employee))
            {
                var employees = _repository.GetEmployeesFuzzy(employee);
                list = list.Where(em => employees.Contains(em.Employee));
            }
            if (!String.IsNullOrEmpty(name))
            {
                var materials = _repository.GetEducationalMaterialsFuzzy(name, Fuzz.PartialRatio);
                list = list.Where(em=>materials.Contains(em));
            }

            ViewBag.EducationalMaterialsList = list.ToList();
            
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMaterial(EducationalMaterialViewModel model, IFormFileCollection uploads)
        {
            try
            {
                Employee employee = _repository.GetEmployee(model.EmployeeFullName);
            }
            catch (Exception)
            {
                ModelState.AddModelError("FullName", "Преподаватель не найден");

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
