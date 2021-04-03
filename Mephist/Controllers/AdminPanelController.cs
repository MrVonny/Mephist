using Mephist.Algorithms;
using Mephist.Data;
using Mephist.Models;
using Mephist.Services;
using Mephist.Services.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly UnitOfWork universityData;
        private readonly IWebHostEnvironment _webHost;

        public AdminPanelController(UnitOfWork universityData, IWebHostEnvironment webHost)
        {
            this.universityData = universityData;
            _webHost = webHost;
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
                EmployeeParser parser = new EmployeeParser(_webHost);
                var employees = parser.GetEmployees();
                foreach (var emp in employees)
                {

                    var employeeToUpdate = await universityData.Employees.FirstOrDefaultAsync(o => o.FullName.Equals(emp.FullName));
                    if (employeeToUpdate == null)
                        await universityData.Employees.AddAsync(emp);
                    else
                    {
                        employeeToUpdate.Departments = emp.Departments;
                        employeeToUpdate.Positions = emp.Positions;
                        employeeToUpdate.Subjects = emp.Subjects;
                        await universityData.Medias.RemoveRange(employeeToUpdate.Photos);                       
                        employeeToUpdate.Photos = emp.Photos;
                        

                        await universityData.Employees.Update(employeeToUpdate);
                    }
                }

            }
            catch (Exception ex)
            {
                return Content(ex.Message + '\n' + ex.StackTrace);
            }
            universityData.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SynchronizeSubjects()
        {
            throw new NotImplementedException();
        }


    }
}
