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
            SaveChanges();
            
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
            return _context.EducationalMaterials.ToList();
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
