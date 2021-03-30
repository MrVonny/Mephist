using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Mephist.Models.Enums
{
    //ToDo:
    //Разграничить лабараторный журнал и лабораторную работу
    public enum EducationalMaterialType
    {
        [Description("Другое")]
        Other=-1,
        [Description("Лекции")]
        Lectures = 1,
        [Description("ДЗ")]
        Homework,
        [Description("Шпоры")]
        CheatSheets,
        [Description("Лабораторный журнал")]
        LaboratoryJournal,
        [Description("Билеты")]
        ExamTickets,
        [Description("Курсовая работа")]
        TermPaper       
    }

    
}