using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Extensions;
using Mephist.Models;
using Mephist.Services;
using Mephist.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using FuzzySharp;
using System.Diagnostics;

namespace Mephist.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IUniversityRepository _repository;
        private readonly IHostEnvironment _environment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public EmployeeController(IUniversityRepository repository, IHostEnvironment environment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _repository = repository;
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public IActionResult Index(string search, int page = 1, int onPage=30)
        {
            List<Employee> employees;
            if (search==null)
                employees = _repository.GetEmployees().OrderBy(x=>x.FullName).ToList();
            else
                employees = _repository.GetEmployeesFuzzy(search).ToList();

            
            
            ViewBag.EmployeesList = employees.Skip((page-1)*onPage).Take(onPage);
            ViewBag.MaxPage = (employees.Count - 1) / onPage + 1;
            ViewBag.Page = page>ViewBag.MaxPage ? ViewBag.MaxPage : page;
            ViewBag.Search = search;

            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) 
                return StatusCode(400);
            Employee model = _repository.GetEmployee(id);            
            return View(model);
        }

        public FileResult GetAvatar(int? employeeId)
        {
            Employee employee = _repository.GetEmployee(employeeId);
            string path;
            if (employee == null || employee.Medias.Count == 0)
                path = Media.DefaultAvatarPath;
            else
                path = employee.Medias.First().GetPath();
            return File($@"Content/{path}", "image/png");
        }

        public IActionResult AddTeacher()
        {
            
            return View();
        }

       
        [HttpGet]
        public IActionResult AddReview()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                
                if (!model.Anonymously && !User.Identity.IsAuthenticated)
                    return RedirectToAction("Login", "Account", new { returnUrl = Request.Path });
                Review review = new Review()
                {
                    CreatedDate = DateTime.Now,
                    Employee = _repository.GetEmployee(model.EmployeeId),
                    Text = model.Text,
                    Anonymously = model.Anonymously
                };
                if (!model.Anonymously)
                    review.User = await _userManager.GetUserAsync(User);

                _repository.CreateReview(review);
                _repository.SaveChanges();
            }
            return RedirectToAction("Details", new { id = model.EmployeeId });
        }

        [HttpGet]
        public JsonResult AutocompleteSearch()
        {
            return Json(_repository.GetEmployees().Select(em =>new { value = em.FullName }));
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("Delete")]
        public void Delete(int? id)
        {
            _repository.DeleteEmployee(id);
        }
    }
}
