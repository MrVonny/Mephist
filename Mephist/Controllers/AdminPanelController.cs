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
        private readonly UniversityData universityData;
        private readonly IWebHostEnvironment _webHost;

        public AdminPanelController(UniversityData universityData, IWebHostEnvironment webHost)
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
                Parallel.ForEach(employees,  (emp) =>
                {
                    Parallel.ForEach(emp.Photos,  (photo) =>
                    {
                        IEnumerable<Media> medias;
                        lock (universityData)
                            medias = universityData.Medias.GetAsync().Result;
                        Media media = null;
                        try
                        {
                            media = medias.SingleOrDefault(o =>
                                    o.EmployeeId.Equals(photo.EmployeeId) &&
                                    o.EducationalMaterialId.Equals(photo.EducationalMaterialId) &&
                                    o.UserId.Equals(photo.UserId) &&
                                    o.PartialMediaPath.Equals(photo.PartialMediaPath) &&
                                    o.MediaName.Equals(photo.MediaName) &&
                                    o.ContentType.Equals(photo.ContentType));
                            lock (universityData)
                                universityData.Medias.Remove(media);
                        }
                        catch(Exception)
                        {

                        }
                        finally
                        {
                            lock (universityData)
                                universityData.Medias.AddAsync(photo).Wait();
                        }
                        
                    });
                    lock (universityData)
                    {
                        var employeeToUpdate = universityData.Employees.FirstOrDefaultAsync(o => o.FullName.Equals(emp.FullName)).Result;
                        if (employeeToUpdate == null)
                            universityData.Employees.AddAsync(emp).Wait();
                        else
                        {
                            employeeToUpdate.Departments = emp.Departments;
                            employeeToUpdate.Positions = emp.Positions;
                            employeeToUpdate.Subjects = emp.Subjects;

                            universityData.Employees.Update(employeeToUpdate);
                        }
                    }
                        
                });

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
