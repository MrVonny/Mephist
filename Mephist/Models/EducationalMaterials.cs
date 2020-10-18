using Mephist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mephist
{
    public class EducationalMaterial
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        [StringLength(4000)]
        public string Description { get; set; }
        public EducationMaterialType Type { get; set; }
        public virtual List<Media> Materials { get; set; }

    }
}