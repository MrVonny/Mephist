using Mephist.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.ViewModels
{
    public class EducationalMaterialViewModel
    {
        [Required(ErrorMessage = "Введите ФИО преподавателя")]
        [Display(Name="Преподаватель")]
        public string EmployeeFullName { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage ="Введите название")]
        [StringLength(200, ErrorMessage = "Не более 250 символов")]
        public string Name { get; set; }
            
        [Display(Name = "Описание")]
        [StringLength(4000, ErrorMessage = "Не более 4000 символов")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Выберите предмет")]
        [Display(Name = "Предмет")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Выберите тип")]
        [Display(Name = "Тип")]
        public EducationalMaterialType Type { get; set; }
        [Display(Name = "Номер работы")]     
        public virtual string Work { get; set; }
        [Range(1, 4, ErrorMessage = "Укажите семестр от 1 до 4")]
        [Display(Name = "Семестр")]       
        public virtual int? Semester { get; set; }
        [Display(Name = "Год")]       
        [Range(1990,2030, ErrorMessage = "Укажите реальную дату")]
        public virtual int? Year { get; set; }
        [Display(Name = "Оценка")]
        [Range(60, 100, ErrorMessage = "Оценка может быть от 60 до 100 баллов")]        
        public virtual int? Mark { get; set; }


    }
}
