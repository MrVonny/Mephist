using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Extensions;
using Mephist.Models;
using Mephist.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        public FileResult GetAvatar(int? employeeId)
        {
            Employee employee = _repository.GetEmployeeById(employeeId);
            string path;
            if (employee.Medias.Count == 0) path = Media.DefaultAvatarPath;
            else path = employee.Medias.First().GetPath();
            return File($@"Content/{path}", "image/png");
        }

        public IActionResult AddTeacher()
        {
            
            return View();
        }
    }
}
