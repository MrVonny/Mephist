using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.ViewModels
{
    public class EducationalMaterialViewModel
    {
        [Required]
        [Display(Name="Преподаватель")]
        public string EmploeeFullName { get; set; }

        [Display(Name = "Название")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Предмет")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Тип")]
        public EducationMaterialType Type { get; set; }
        [Display(Name="Номер работы")]
        public virtual string Work { get; set; }
        [Range(1, 4)]
        [Display(Name = "Семестр")]
        public virtual int? Semester { get; set; }
        [Display(Name="Год")]
        public virtual int? Year { get; set; }
        [Display(Name="Оценка")]
        [Range(60, 100)]
        public virtual int? Mark { get; set; }

    }
}
