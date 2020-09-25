using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public class Teacher
    {
        [Display(Name = "ФИО")]
        [Required]
        //[RegularExpression(@"[А-Я][а-я]+\s[А-Я][а-я]+\s[А-Я][а-я]")]
        [RegularExpression(@"^[A-Z]+")]
        public string FullName { get; set; }
    }
}
