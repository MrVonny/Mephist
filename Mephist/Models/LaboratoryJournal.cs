﻿using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public class LaboratoryJournal : EducationalMaterial
    {
        [Required]
        public virtual string Work { get; set; }
        [Required]
        [Range(1,6)]
        public virtual int Semester { get; set; }
        [Range(1990,2025)]
        public virtual int? Year { get; set; }
        [Range(0,100)]
        public virtual int? Mark { get; set; }
    }
}
