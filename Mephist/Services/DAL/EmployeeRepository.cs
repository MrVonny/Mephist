using Mephist.Data;
using Mephist.Extensions;
using Mephist.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services.DAL
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private UniversityContext context;
        public EmployeeRepository(UniversityContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Employee> GetByFullNameAsync(string fullName)
        {
            return await context.Employees.SingleOrDefaultAsync(x => x.FullName.Equals(fullName));
        }

        public async Task<IEnumerable<Employee>> GetEmployeesFuzzyAsync(string fullName)
        {
            fullName = fullName.Transliterate().ToLower();
            IEnumerable<Employee> employees = await context.Employees.ToListAsync();

             employees = employees.Select(x => new { sim = FuzzySharp.Fuzz.PartialRatio(fullName, x.FullName.Transliterate().ToLower()), Employee = x })
                .OrderByDescending(x => x.sim)
                .Where(x => x.sim > 76)
                .Select(x => x.Employee);
            return employees;
        }
    }
}
