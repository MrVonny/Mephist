using Mephist.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mephist.Services.DAL
{
    public interface IEducationalMaterialRepository
    {
        Task<IEnumerable<EducationalMaterial>> GetEducationalMaterialsFuzzyAsync(string name);
    }
}