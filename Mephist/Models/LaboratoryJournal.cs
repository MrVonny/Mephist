using Microsoft.VisualBasic;
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
        [Column(TypeName = "nvarchar(20)")]
        public virtual string Work { get; set; }
        [Range(1,4)]
        public virtual int Semester { get; set; }
        public virtual int Year { get; set; }
        [Range(0,100)]
        public virtual int Mark { get; set; }
    }
}
