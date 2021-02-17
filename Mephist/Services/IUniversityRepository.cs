using Mephist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mephist.Services
{
    public interface IUniversityRepository
    {
        //TODO: IEnumerable and IQueryable      
        //Статическая инфа

        IEnumerable<Employee> GetEmployees();
        //IEnumerable<Employee> GetEmployeesFuzzy(string fullName, Func<string, string, int> compareFunc, int similarity=50);
        IEnumerable<Employee> GetEmployeesFuzzy(string fullName);
        Employee GetEmployee(string fullName);
        Employee GetEmployee(int? id);
        void CreateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        void DeleteEmployee(int? id);
        void DeleteEmployee(string fullname);
        bool ExistsEmployee(string fullname);
        void UpdateEmployee(int id, Employee newEmployee);
        void UpdateEmployee(string fullName, Employee newEmployee);


        IEnumerable<LaboratoryJournal> GetLaboratoryJournals();
        LaboratoryJournal GetLaboratoryJournal(int? id);
        void CreateLaboratoryJournal(LaboratoryJournal labJournal);
        void DeleteLaboratoryJournal(int? id);
        void DeleteLaboratoryJournal(LaboratoryJournal labJournal);

        IEnumerable<EducationalMaterial> GetEducationalMaterials();
        IEnumerable<EducationalMaterial> GetEducationalMaterialsFuzzy(string name, Func<string, string, int> compareFunc, int similarity = 50);
        EducationalMaterial GetEducationalMaterial(int? id);
        void CreateEducationalMaterial(EducationalMaterial educationalMaterial);
        void DeleteEducationalMaterial(EducationalMaterial educationalMaterial);
        void DeleteEducationalMaterial(int? id);

        IEnumerable<Media> GetMedia();
        Media GetMedia(int? id);
        void CreateMedia(Media media);
        void CreateMediaRange(IEnumerable<Media> medias);
        void CreateMediaRange(Media[] medias);
        void DeleteMedia(int? id);
        void DeleteMedia(Media media);

        IEnumerable<Review> GetReviews();
        IEnumerable<Review> GetReviews(Employee employee);
        IEnumerable<Review> GetReviews(User user);
        IEnumerable<Review> GetReviews(string employeFullName);
        Review GetReview(int? id);
        void CreateReview(Review review);
        void DeleteReview(Review review);
        void DeleteReview(int? id);

       
        User GetUserById(string id);
        User GetUserByEmail(string email);
        User GetUserByUserName(string userName);


        void SaveChanges();

    }
}
