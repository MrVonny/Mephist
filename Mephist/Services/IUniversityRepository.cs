using Mephist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services
{
    public interface IUniversityRepository
    {
        //TODO: IEnumerable to IQueryable
        IEnumerable<Employee> GetEmployees();
        //IEnumerable<EducationalMaterial> GetEducationalMaterials();
        Employee GetEmployeeByName(string fullName);
        Employee GetEmployeeById(int? id);
        Employee CreateEmployee(Employee employee);

        IEnumerable<LaboratoryJournal> GetLaboratoryJournals();
        void CreateLaboratoryJournal(LaboratoryJournal labJournal);
        void DeleteLaboratoryJournalById(int? id);

        IEnumerable<EducationalMaterial> GetEducationalMaterials();
        void CreateEducationalMaterial(EducationalMaterial educationalMaterial);
        void DeleteEducationalMaterialById(int? id);

        void SaveChanges();

    }
}
