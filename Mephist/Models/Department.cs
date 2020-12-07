using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        public int? InstituteId { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual IEnumerable<Employee> Employees { get; set; }
    }
}
