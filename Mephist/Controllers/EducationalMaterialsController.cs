using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Data;
using Mephist.Models;
using Mephist.Services;
using Mephist.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mephist.Controllers
{
    public class EducationalMaterialsController : Controller
    {

        private IUniversityRepository _repository;

        public EducationalMaterialsController(IUniversityRepository repository)
        {
            _repository = repository;
        }

        
        public IActionResult GetMaterials(EducationMaterialType[] types, string[] employees)
        {
            IEnumerable<EducationalMaterial> educationalMaterials = _repository.GetEducationalMaterials();
            if (types != null)
                educationalMaterials=educationalMaterials.Where(em => types.Contains(em.Type));
            if (employees != null)
                educationalMaterials=educationalMaterials.Where(em => employees.Contains(em.Employee.FullName));
            List<EducationalMaterial> list = educationalMaterials.ToList();
            ViewBag.EducationalMaterialsList = list;
            return View();
        }

        [Authorize]
        public IActionResult AddMaterial()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddMaterial(EducationalMaterialViewModel model)
        {
            if(model.Type==EducationMaterialType.LaboratoryJournal)
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
            if(ModelState.IsValid)
            {
                
                if(model.Type==EducationMaterialType.LaboratoryJournal)
                {
                    LaboratoryJournal lj = new LaboratoryJournal()
                    {
                        Name = model.Name,
                        Employee = _repository.GetEmployeeByName(model.EmploeeFullName),                    
                        Description=model.Description,
                        Subject=model.Subject,
                        Type=model.Type,

                        Mark = model.Mark ?? throw new Exception("Wrong model"),
                        Semester = model.Semester ?? throw new Exception("Wrong model"),
                        Work = model.Work,
                        Year = model.Year ?? throw new Exception("Wrong model")
                    };
                    _repository.CreateLaboratoryJournal(lj);
                    _repository.SaveChanges();
                }
                else
                {
                    EducationalMaterial em = new EducationalMaterial()
                    {
                        Name = model.Name,
                        Employee = _repository.GetEmployeeByName(model.EmploeeFullName),
                        Description = model.Description,
                        Subject = model.Subject,
                        Type = model.Type
                    };
                    _repository.CreateEducationalMaterial(em);
                    _repository.SaveChanges();
                }

                return RedirectToAction("GetMaterials", "EducationalMaterial");
            }

            return View(model);
        }
    }
}
