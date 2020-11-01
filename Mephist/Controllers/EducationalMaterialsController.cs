using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Data;
using Mephist.Services;
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
            IQueryable<EducationalMaterial> educationalMaterials = _repository.GetEducationalMaterials();
            if (types != null)
                educationalMaterials=educationalMaterials.Where(em => types.Contains(em.Type));
            if (employees != null)
                educationalMaterials=educationalMaterials.Where(em => employees.Contains(em.Employee.FullName));
            List<EducationalMaterial> list = educationalMaterials.ToList();
            ViewBag.EducationalMaterialsList = list;
            return View();
        }
    }
}
