﻿using System;
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
    public partial class EducationalMaterialsController : Controller
    {

        public async Task<IActionResult> GetLabJournals()
        {
            var model = await universityData.LaboratoryJournals.GetAsync();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddLabJournal(EducationalMaterialViewModel model, string query, IFormFileCollection uploads)
        {
            List<Media> medias = new List<Media>();

                Employee employee = await universityData.Employees.GetByFullNameAsync(model.EmployeeFullName);
            if(employee==null)
                ModelState.AddModelError("FullName", "Преподаватель не найден");
            
            if (model.Work == null)
                ModelState.AddModelError("Work", "Введите название работы");

            if (uploads.Count <= 0)
                ModelState.AddModelError("Files", "Не загржен ни один файл");
            
            try
            {
                model.Semester = _universityStaticData.GetSemestrBySubject(model.Subject);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Work", "Нет такой работы");
            }
                

            if (ModelState.IsValid)
            {
                LaboratoryJournal lj = new LaboratoryJournal()
                {
                    Name = model.Name,
                    Employee = await universityData.Employees.GetByFullNameAsync(model.EmployeeFullName),
                    Description = model.Description,
                    Subject = model.Subject,
                    Type = model.Type,
                    Materials = medias,

                    Semester = model.Semester ?? throw new ArgumentNullException(),
                    Work = model.Work ?? throw new ArgumentNullException(),
                    Mark = model.Mark,
                    Year = model.Year
                };
                await universityData.LaboratoryJournals.AddAsync(lj);
                var storage = new AwsS3Storage();
                var fileNames = query.Split('/');
                for (int i = 0; i < fileNames.Length; i++)
                {
                    var uploadfileName = fileNames[i];
                    var file = uploads.Single(x=>x.FileName==uploadfileName);

                    var key = (i + 1) + "_" + $"{DateTime.Now:O}." + file.FileName.Split('.').Last();
                    medias.Add(new Media(lj,
                        key, key, file.ContentType, await _userManager.GetUserAsync(User)));
                    await storage.AddItem(file.OpenReadStream(), key, file.ContentType);
                }

                foreach (var media in medias)
                    media.EducationalMaterial = lj;
                foreach (var media in medias)
                   await universityData.Medias.AddAsync(media);
                await universityData.SaveAsync();

                return RedirectToAction("GetMaterials", "EducationalMaterials");
            }
            ViewBag.Types = Types;
            return View("AddMaterial", model);
        }
       
    }
}
