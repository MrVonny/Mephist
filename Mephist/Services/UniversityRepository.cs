using Mephist.Data;
using Mephist.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mephist.Services
{
    public class UniversityRepository : IUniversityRepository
    {
        private UniversityContext _context;
        public UniversityRepository(UniversityContext context)
        {
            _context = context;
        }

        public Employee CreateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EducationalMaterial> GetEducationalMaterials()
        {
            return _context.EducationalMaterials.ToList();
        }

        public Employee GetEmployeeById(int? id)
        {
            return _context.Employees.Single(emp => id==emp.Id);
        }

        public Employee GetEmployeeByName(string fullName)
        {
            return _context.Employees.Single(emp => fullName.Equals(emp));
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
