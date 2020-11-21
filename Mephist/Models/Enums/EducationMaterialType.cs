using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mephist.Models.Enums
{
    public enum EducationMaterialType
    {
        [Display(Name ="Другое")]
        Other=-1,
        [Display(Name = "Лекции")]
        Lectures =1,
        [Display(Name = "ДЗ")]
        Homework,
        [Display(Name = "Шпоры")]
        Spurs,
        [Display(Name = "Лабараторный журнал")]
        LaboratoryJournal,
        [Display(Name = "Билеты")]
        ExamTickets,
        [Display(Name = "Курсовая работа")]
        TermPaper       
    }
}