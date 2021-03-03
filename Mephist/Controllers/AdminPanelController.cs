using Mephist.Algorithms;
using Mephist.Data;
using Mephist.Models;
using Mephist.Services;
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
        private readonly IUniversityRepository _repository;
        private readonly IWebHostEnvironment _webHost;

        public AdminPanelController(IUniversityRepository repository, IWebHostEnvironment webHost)
        {          
            _repository = repository;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SynchronizeEmployees()
        {
            try
            {
                EmployeeParser parser = new EmployeeParser(_webHost);
                var employees = parser.GetEmployees();
                Parallel.ForEach(employees, (emp) =>
                {
                    Parallel.ForEach(emp.Photos, (photo) =>
                    {
                        IEnumerable<Media> medias;
                        lock (_repository)
                            medias = _repository.GetMedia();
                        Media media = null;
                        try
                        {
                            media = medias.Single(o =>
                                    o.EmployeeId.Equals(photo.EmployeeId) &&
                                    o.EducationalMaterialId.Equals(photo.EducationalMaterialId) &&
                                    o.UserId.Equals(photo.UserId) &&
                                    o.PartialMediaPath.Equals(photo.PartialMediaPath) &&
                                    o.MediaName.Equals(photo.MediaName) &&
                                    o.ContentType.Equals(photo.ContentType));
                            lock (_repository)
                                _repository.DeleteMedia(media);
                        }
                        catch(Exception)
                        {

                        }
                        finally
                        {
                            lock (_repository)
                                _repository.CreateMedia(photo);
                        }
                        
                    });
                    lock (_repository)
                        if (!_repository.ExistsEmployee(emp.FullName))
                            _repository.CreateEmployee(emp);
                        else
                            _repository.UpdateEmployee(emp.FullName, emp);
                });

            }
            catch (Exception ex)
            {
                return Content(ex.Message + '\n' + ex.StackTrace);
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
