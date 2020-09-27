using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models.Services
{
    public interface IUniversityData
    {
        void EmployeesInitializeData();
        void InstitutionsInitializeData();
        void DepartmentsInitializeData();
        void SubjectsInitializeData();
    }
}
