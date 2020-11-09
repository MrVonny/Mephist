using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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

        public IEnumerable<EducationalMaterial> GetEducationalMaterials()
        {
            throw new NotImplementedException();
        }

        public EducationalMaterial GetEducationalMaterial(int? id)
        {
            throw new NotImplementedException();
        }

        public void CreateEducationalMaterial(EducationalMaterial educationalMaterial)
        {
            throw new NotImplementedException();
        }


        public void DeleteEducationalMaterial(EducationalMaterial educationalMaterial)
        {
            throw new NotImplementedException();
        }

        public void DeleteEducationalMaterialById(int? id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LabJournal

        public IEnumerable<LaboratoryJournal> GetLaboratoryJournals()
        {
            throw new NotImplementedException();
        }

        public LaboratoryJournal GetLaboratoryJournal(int? id)
        {
            throw new NotImplementedException();
        }

        public void CreateLaboratoryJournal(LaboratoryJournal labJournal)
        {
            throw new NotImplementedException();
        }

        public void DeleteLaboratoryJournal(int? id)
        {
            throw new NotImplementedException();
        }

        public void DeleteLaboratoryJournal(LaboratoryJournal labJournal)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Employee

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee GetEmployee(string fullName)
        {
            return _context.Employees.Single(em => fullName.Equals(em.FullName));
        }
        public  Employee GetEmployee(int? id)
        {
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


        #endregion

        #region Media

        public IEnumerable<Media> GetMedia()
        {
            throw new NotImplementedException();
        }

        public Media GetMedia(int? id)
        {
            throw new NotImplementedException();
        }

        public void CreateMedia(Media media)
        {
            throw new NotImplementedException();
        }

        public void CreateMedia(IEnumerable<Media> medias)
        {
            throw new NotImplementedException();
        }

        public void DeleteMedia(int? id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMedia(Media media)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Reviews

        public IEnumerable<Review> GetReviews()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetReviews(Employee employe)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetReviews(string employeFullName)
        {
            throw new NotImplementedException();
        }

        public Review GetReview(int? id)
        {
            throw new NotImplementedException();
        }

        public void CreateReview(Review review)
        {
            throw new NotImplementedException();
        }


        public void DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(int? id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Ratings

        public IEnumerable<Rating> GetRating()
        {
            return _context.Ratings.ToList();
        }

        public Rating GetRating(int? id)
        {
            return _context.Ratings.Single(x => x.Id == id);
        }

        public void CreateRating(Rating rating)
        {
            _context.Ratings.Add(rating);
        }


        public void DeleteRating(int? id)
        {
            DeleteRating(GetRating(id));
        }

        public void DeleteRating(Rating rating)
        {
            _context.Ratings.Remove(rating);
        }

        #endregion


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
