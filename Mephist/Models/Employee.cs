using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    //ToDo: Реализовать атрибуты
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }      
        public List<string> Positions { get; set; }
        public List<string> Departments { get; set; }    
        public List<string> Subjects { get; set; }
        public virtual List<EducationalMaterial> EducationalMaterials { get; set; } = new List<EducationalMaterial>();
        public virtual List<Review> Reviews { get; set; } = new List<Review>();
        public virtual List<Media> Photos { get; set; } = new List<Media>();

        public string GetAvatarPath()
        {
            return "~/" + (Photos.Count == 0 ? Media.DefaultAvatarPath : Photos.First().GetPath());
        }

        #region Constructors

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

        #region Fields


        #endregion

    }
}
