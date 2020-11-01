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
        IQueryable<EducationalMaterial> GetEducationalMaterials();

        void SaveChanges();
        //Cupcake GetCupcakeById(int id);
        //void CreateCupcake(Cupcake cupcake);
        //void DeleteCupcake(int id);
        //
        //IQueryable<Bakery> PopulateBakeriesDropDownList();
    }
}
