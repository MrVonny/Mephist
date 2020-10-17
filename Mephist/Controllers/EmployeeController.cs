using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Extensions;
using Mephist.Models;
using Mephist.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Mephist.Controllers
{
    public class EmployeeController : Controller
    {

        private IUniversityRepository _repository;
        private IHostEnvironment _environment;

        public EmployeeController(IUniversityRepository repository, IHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>(_repository.GetEmployees());
            ViewBag.EmployeesList = employees;
            

            return View();
        }

        [HttpGet]
        [Route("Employee/Details/{id}")]
        public IActionResult Details(int? id)
        {
            //if (id == null) return NotFound();
            Employee model = _repository.GetEmployeeById(id);
            //if (model == null) return NotFound();

            

            return View(model);
        }


        public string GetPhotoAvatarPath(Employee employee)
        {
            return @"~\Content\Shared\DefaultAvatar.jpg";

        }

        public IActionResult AddTeacher()
        {
            
            return View();
        }
    }
}
