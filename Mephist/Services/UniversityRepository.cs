using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mephist.Services
{
    public class UniversityRepository : IUniversityRepository
    {
        private UniversityContext _context;
        public UniversityRepository(UniversityContext context)
        {


            _context = context;
            /*
            Employee samedov = new Employee()
            {
                
                FullName = "Самедов Виктор Витальевич",
                Department = "40",
                Institutions = "ИЯФиТ;ИИКС",
                Subjects = "Предмет 1;Предмет 2"

            };

            _context.Employees.Add(samedov);
            _context.SaveChanges();
            User user1 = new User()
            {
                
                NickName = "MEPhI Killer"
            };

            _context.Users.Add(user1);
            _context.SaveChanges();

            Media mediaSamedov = new Media()
            {
                
                PartialMediaPath = Path.Combine(@"\Employees\",
                                   String.Concat("Самедов Виктор Витальевич".Transliterate().Split(' '))),
                MediaName = "1131_cps.jpg",
                EmployeeId = 1
            };
            _context.Medias.Add(mediaSamedov);
            _context.SaveChanges();
            EducationalMaterial em1 = new EducationalMaterial
            {

                Type = EducationMaterialType.ExamTickets,
                Name = "Билеты за 3 14888 год",
                Employee = samedov
            };
            _context.Add(em1);
            _context.SaveChanges();

            Media mediaBilets = new Media()
            {
                CreatedDate = DateTime.Now,
                PartialMediaPath = @"\EducationMaterials\",
                EducationalMaterial = em1

            };
            _context.Medias.Add(mediaBilets);
            _context.SaveChanges();
            Rating rating1 = new Rating()
            {
                CharacterScore = 5,
                CharacterVotes = 4,
                ExamsScore = -10,
                ExamsVotes = 5,
                TeachingScore = 28,
                TeachingVotes = 15,
                Employee= samedov

            };
            _context.Add(rating1);
            _context.SaveChanges();
            Review reviewSame = new Review()
            {
               
                Anonymously = false,
                User = user1,
                Text = "АААААА ПРЕПОООООД ТОПППППППП 11/10",
                CreatedDate = DateTime.Now,
                Employee=samedov

            };
            _context.Add(reviewSame);
            _context.SaveChanges();
            */

        }

        #region EducationalMaterial
        public void CreateEducationalMaterial(EducationalMaterial educationalMaterial)
        {
            throw new NotImplementedException();
        }

        public void DeleteEducationalMaterialById(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EducationalMaterial> GetEducationalMaterials()
        {
            return _context.EducationalMaterials;
        }
        #endregion

        #region LabJournal
        public void CreateLaboratoryJournal(LaboratoryJournal labJournal)
        {
            throw new NotImplementedException();
        }

        public void DeleteLaboratoryJournalById(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LaboratoryJournal> GetLaboratoryJournals()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Employee
        public Employee CreateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeById(int? id)
        {
            return _context.Employees.Single(emp => id == emp.Id);
        }

        public Employee GetEmployeeByName(string fullName)
        {
            return _context.Employees.Single(emp => fullName.Equals(emp));
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }
        #endregion

        #region

        #endregion

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
