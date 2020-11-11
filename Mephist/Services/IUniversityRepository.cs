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
        //TODO: IEnumerable to IQueryable      

        //Read
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(string fullName);
        Employee GetEmployee(int? id);
        //Create
        void CreateEmployee(Employee employee);
        //Delete
        void DeleteEmployee(Employee employee);
        void DeleteEmployee(int? id);
        void DeleteEmployee(string fullname);


        IEnumerable<LaboratoryJournal> GetLaboratoryJournals();
        LaboratoryJournal GetLaboratoryJournal(int? id);
        void CreateLaboratoryJournal(LaboratoryJournal labJournal);
        void DeleteLaboratoryJournal(int? id);
        void DeleteLaboratoryJournal(LaboratoryJournal labJournal);

        IEnumerable<EducationalMaterial> GetEducationalMaterials();
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

        IEnumerable<Rating> GetRating();
        Rating GetRating(int? id);
        void CreateRating(Rating rating);
        void DeleteRating(int? id);
        void DeleteRating(Rating rating);



        void SaveChanges();

    }
}
