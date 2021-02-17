using Mephist.Algorithms;
using Mephist.Data;
using Mephist.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminPanelController : Controller
    {
        private readonly IUniversityRepository _repository;

        public AdminPanelController(IUniversityRepository repository)
        {          
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SynchronizeEmployees()
        {
            try
            {
                EmployeeParser parser = new EmployeeParser();
                var employees = await parser.GetEmployees();
                foreach (var emp in employees)
                {
                    if (!_repository.ExistsEmployee(emp.FullName))
                        _repository.CreateEmployee(emp);
                    else
                        _repository.UpdateEmployee(emp.FullName, emp);
                    
                }
                
            }
            catch(Exception ex)
            {
                return Content(ex.Message + '\n' +ex.StackTrace);
            }
            _repository.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SynchronizeSubjects()
        {
            throw new NotImplementedException();
        }


    }
}
