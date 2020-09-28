using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        [Column(TypeName ="nvarchar(50)")]
        public string FullName { get; set; }
        //[Institute]
        public ICollection<string> Institutions { get; set; }
        //[Department]
        public string Department { get;  set; }
        //[Subject]
        public ICollection<string> Subjects { get; set; }
        public virtual ICollection<EducationalMaterial> EducationalMaterials { get; set; }
        //[Reviews]
        public virtual ICollection<Review> Reviews { get; set; }
        public int? PhotoAvatarId { get; set; }
        public virtual Media PhotoAvatar { get; set; }
        public virtual ICollection<Media> Photos { get; set; }
        

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
