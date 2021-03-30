using Mephist.Data;
using Mephist.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services.DAL
{
    public class EducationalMaterialRepository : GenericRepository<EducationalMaterial>, IEducationalMaterialRepository
    {
        private UniversityContext context;
        public EducationalMaterialRepository(UniversityContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<EducationalMaterial>> GetEducationalMaterialsFuzzyAsync(string name)
        {
            name = name.Transliterate().ToLower();
            IEnumerable<EducationalMaterial> employees = await context.EducationalMaterials.ToListAsync();

            employees = employees.Select(x => new { sim = FuzzySharp.Fuzz.PartialRatio(name, x.Name.Transliterate().ToLower()), EducationalMaterial = x })
               .OrderByDescending(x => x.sim)
               .Where(x => x.sim > 70)
               .Select(x => x.EducationalMaterial);
            return employees;
        }

    }
}
