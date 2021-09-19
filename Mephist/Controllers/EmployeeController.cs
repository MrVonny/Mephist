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
using HeyRed.Mime;
using Mephist.Services.DAL;

namespace Mephist.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly UnitOfWork universityData;
        private readonly IHostEnvironment _environment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public EmployeeController(UnitOfWork universityData, IHostEnvironment environment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.universityData = universityData;
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public async Task<IActionResult> Index(string search, int page = 1, int onPage=30)
        {
            List<Employee> employees;
            
            if (search==null)
                employees = (await universityData.Employees.GetAsync()).OrderBy(x=>x.FullName).ToList();
            else
                employees = (await universityData.Employees.GetEmployeesFuzzyAsync(search)).ToList();

            
            
            ViewBag.EmployeesList = employees.Skip((page-1)*onPage).Take(onPage);
            ViewBag.MaxPage = (employees.Count - 1) / onPage + 1;
            ViewBag.Page = page>ViewBag.MaxPage ? ViewBag.MaxPage : page;
            ViewBag.Search = search;

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) 
                return StatusCode(400);
            Employee model = await universityData.Employees.GetByIdAsync(id.Value);            
            return View(model);
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
                    Employee = await universityData.Employees.GetByIdAsync(model.EmployeeId),
                    Text = model.Text,
                    Anonymously = model.Anonymously
                };
                if (!model.Anonymously)
                    review.User = await _userManager.GetUserAsync(User);

                await universityData.Reviews.AddAsync(review);
                await universityData.SaveAsync();
            }
            return RedirectToAction("Details", new { id = model.EmployeeId });
        }

        

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("Delete")]
        public async void Delete(int? id)
        {
            if (id != null)
            {
                await universityData.Employees.Remove(await universityData.Employees.GetByIdAsync(id.Value));
                await universityData.SaveAsync();
            }
                
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        //ToDo:
        //Убрать returnUrl и сделать удаление через AJAX.
        public async Task<IActionResult> Delete(int? id, string returnUrl)
        {
            if (id is null)
                return StatusCode(400);
            await universityData.Reviews.Remove(await universityData.Reviews.GetByIdAsync(id.Value));
            await universityData.SaveAsync();
            return RedirectToAction("GetMaterials", "EducationalMaterials");
        }


    }
}
