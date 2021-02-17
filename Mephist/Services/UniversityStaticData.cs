using Mephist.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services
{
    public class UniversityStaticData
    {
        private readonly UniversityContext _context;

        private List<string> subjects = new List<string>();
        public UniversityStaticData(UniversityContext context)
        {
            _context = context;
            foreach (var list in _context.Employees.Select(em => em.Subjects))
                if(list!=null) subjects.AddRange(list);
            subjects = subjects.Distinct().ToList();
        }

        public List<string> GetSubjects()
        {
            return subjects;
        }

    }
}
