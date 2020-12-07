using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public class Institute
    {
        [Key]
        public string Name { get; set; }
        public virtual IEnumerable<Department> Departments { get; set; }
    }
}
