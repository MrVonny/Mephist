using Mephist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services.DAL
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
        Task<Employee> GetByFullNameAsync(string fullName);
        Task<IEnumerable<Employee>> GetEmployeesFuzzyAsync(string fullName);
    }
}
