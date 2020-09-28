using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    //ToDo: Реализовать атрибуты
    public class Employee
    {
        #region Properties

        [Display(Name = "ФИО")]
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        //[Institute]
        public List<string> Institutions { get; set; }
        //[Department]
        public string Department { get;  set; }
        //[Subject]
        public List<string> Subjects { get; set; }
        
        public List<EducationalMaterial> EducationalMaterials { get; set; }
        //[Reviews]
        public List<Review> Reviews { get; set; }

        public IFormFile PhoroAvatar { get; set; }
        public string ImageName { get; set; }
        public byte[] PhotoFile { get; set; }
        public string ImageMimeType { get; set; }
        

        #endregion

        #region Constructors
        public Employee(string fullName)
        {
            FullName = fullName;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

        #region Fields


        #endregion

    }
}
