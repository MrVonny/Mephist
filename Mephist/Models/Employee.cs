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
        #region Properties
        [Key]
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }
        public virtual IEnumerable<Department> Departments { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Subjects { get; set; } = "";
        public virtual List<EducationalMaterial> EducationalMaterials { get; set; } = new List<EducationalMaterial>();
        public virtual List<Review> Reviews { get; set; } = new List<Review>();
        //public int? PhotoAvatarId { get; set; }
        //public virtual Media PhotoAvatar { get; set; }
       
        public virtual List<Media> Medias { get; set; } = new List<Media>();
        public virtual Rating Rating { get; set; } = new Rating();


        #endregion

        #region Constructors


        public string GetAvatarPath()
        {
            
            if (Medias.Count == 0) return Media.DefaultAvatarPath;
            return Medias.First().GetPath();
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
