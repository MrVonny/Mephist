using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DuoVia;
using DuoVia.FuzzyStrings;

namespace Mephist.Services
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly UniversityContext _context;
        //UserManager<User> _userManager;
        //RoleManager<IdentityRole> _roleManager;
        public UniversityRepository(UniversityContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            
            string adminEmail = "borafon@gmail.com";
            string password = "MrBen228";
            if (roleManager.FindByNameAsync("admin").Result == null)
            {
                 roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if ( userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                User admin = new User { Email = adminEmail, UserName = "Admin" };
                
                IdentityResult result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                     userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }

        #region EducationalMaterial

        public IEnumerable<EducationalMaterial> GetEducationalMaterials()
        {
            return _context.EducationalMaterials.ToList();
        }

        public IEnumerable<EducationalMaterial> GetEducationalMaterialsFuzzy(string name, Func<string, string, int> compareFunc, int similarity = 50)
        {
            var materials = _context.EducationalMaterials.ToList()
                .Select(x => new { sim = compareFunc(x.Name, name), Name = x })
                .OrderByDescending(x => x.sim)
                .Where(x => x.sim > similarity)
                .Select(x => x.Name);
            return materials;
        }
        public EducationalMaterial GetEducationalMaterial(int? id)
        {
            return _context.EducationalMaterials.Single(x => x.Id == id);
        }

        public void CreateEducationalMaterial(EducationalMaterial educationalMaterial)
        {
            _context.EducationalMaterials.Add(educationalMaterial);
        }


        public void DeleteEducationalMaterial(EducationalMaterial educationalMaterial)
        {
            educationalMaterial.Materials.RemoveAll(x => true);
            _context.EducationalMaterials.Remove(educationalMaterial);
        }

        public void DeleteEducationalMaterial(int? id)
        {
            DeleteEducationalMaterial(GetEducationalMaterial(id));
        }

        #endregion

        #region LabJournal

        public IEnumerable<LaboratoryJournal> GetLaboratoryJournals()
        {
            return _context.LaboratoryJournals.ToList();
        }

        public LaboratoryJournal GetLaboratoryJournal(int? id)
        {
            return _context.LaboratoryJournals.Single(x=>x.Id==id);
        }

        public void CreateLaboratoryJournal(LaboratoryJournal labJournal)
        {
            _context.LaboratoryJournals.Add(labJournal);
        }

        public void DeleteLaboratoryJournal(int? id)
        {
            DeleteLaboratoryJournal(GetLaboratoryJournal(id));
        }

        public void DeleteLaboratoryJournal(LaboratoryJournal labJournal)
        {
            _context.LaboratoryJournals.Remove(labJournal);
        }

        #endregion

        #region Employee

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }
        public IEnumerable<Employee> GetEmployeesFuzzy(string fullName)
        {
            fullName = fullName.Transliterate().ToLower();
            var employees = _context.Employees.ToList()
                .Select(x => new { sim = FuzzySharp.Fuzz.PartialRatio(fullName, x.FullName.Transliterate().ToLower()), Employee = x })
                .OrderByDescending(x => x.sim)
                .Where(x => x.sim > 76)
                .Select(x => x.Employee);
            return employees;
        }
        public Employee GetEmployee(string fullName)
        {
            return _context.Employees.First(em => fullName.Equals(em.FullName));
            
        }
        public  Employee GetEmployee(int? id)
        {
            if (id == null)
                return null;
            return _context.Employees.Single(em =>em.Id==id);
        }
        //Create
        public void CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);   
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public void DeleteEmployee(int? id)
        {
            DeleteEmployee(GetEmployee(id));
        }

        public void DeleteEmployee(string fullname)
        {
            DeleteEmployee(GetEmployee(fullname));
        }

        public bool ExistsEmployee(string fullname)
        {
            return _context.Employees.Any(em=>em.FullName.Equals(fullname));
        }

        public void UpdateEmployee(string fullName, Employee newEmployee)
        {
            Employee old = GetEmployee(fullName);

            old.Departments = newEmployee.Departments;
            old.Positions = newEmployee.Positions;
            old.Subjects = newEmployee.Subjects;

            if (newEmployee.Photos != null)
                old.Photos.AddRange(newEmployee.Photos);
            if (newEmployee.Reviews != null)
                old.Reviews.AddRange(newEmployee.Reviews);
            if (newEmployee.EducationalMaterials != null)
                old.EducationalMaterials.AddRange(newEmployee.EducationalMaterials);
        }

        public void UpdateEmployee(int id, Employee newEmployee)
        {

        }

        #endregion

        #region Media

        public IEnumerable<Media> GetMedia()
        {
            return _context.Medias.ToList();
        }

        public Media GetMedia(int? id)
        {
            return _context.Medias.Single(x => x.Id == id);
        }

        public void CreateMedia(Media media)
        {
            _context.Medias.Add(media);
        }

        public void CreateMediaRange(IEnumerable<Media> medias)
        {
            _context.Medias.AddRange(medias);
        }

        public void CreateMediaRange(Media[] medias)
        {
            _context.Medias.AddRange(medias);
        }

        public void DeleteMedia(int? id)
        {
            DeleteMedia(GetMedia(id));
        }

        public void DeleteMedia(Media media)
        {
            _context.Medias.Remove(media);
        }

        

        #endregion

        #region Reviews

        public IEnumerable<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public IEnumerable<Review> GetReviews(Employee employee)
        {
            return _context.Reviews.Where(em => em.EmployeeId == employee.Id).ToList();
        }

        public IEnumerable<Review> GetReviews(User user)
        {
            return _context.Reviews.Where(em => em.UserId == user.Id).ToList();
        }

        public IEnumerable<Review> GetReviews(string employeFullName)
        {
            Employee employee = GetEmployee(employeFullName);
            return _context.Reviews.Where(em => em.EmployeeId == employee.Id).ToList();
        }

        public Review GetReview(int? id)
        {
            return _context.Reviews.Single(x => x.Id == id);
        }

        public void CreateReview(Review review)
        {
            _context.Reviews.Add(review);
        }


        public void DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
        }

        public void DeleteReview(int? id)
        {
            DeleteReview(GetReview(id));
        }

        #endregion

        #region User
        
        public User GetUserById(string id)
        {
           return _context.Users.Single(x => id.Equals(x.Id));
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Single(x => email.ToUpper().Equals(x.NormalizedEmail));
            
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.Single(x => userName.ToUpper().Equals(x.UserName.Normalize()));
        }
        #endregion

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        
    }
}
