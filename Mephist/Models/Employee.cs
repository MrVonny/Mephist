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
        public IReadOnlyList<string> Institutions
        {
            get
            {
                return institutions;
            }

            set
            {
                institutions = new List<string>(value);
            }
        }

        //[Department]
        public string Department { get;  set; }
        //[Subject]
        public IReadOnlyList<string> Subjects {
            get
            {
                return subjects;
            }

            set
            {
                institutions = new List<string>(value);
            }
        }
        public List<EducationalMaterial> EducationalMaterials { get; set; }
        //[Reviews]
        public IReadOnlyList<Review> Reviews { get; set; }
        public MediaFile Photo { get; set; }

        #endregion

        #region Constructors
        public Employee(string fullName)
        {
            FullName = fullName;
        }

        public Employee(string fullName, IReadOnlyList<string> institutions, string department,
            IReadOnlyList<string> subjects, List<EducationalMaterial> educationalMaterials,
            IReadOnlyList<Review> reviews) : this(fullName)
        {
            Institutions = institutions;
            Department = department;
            Subjects = subjects;
            EducationalMaterials = educationalMaterials;
            Reviews = reviews;
        }




        #endregion

        #region Public Methods
        public void AddSubject(string subject)
        {
            //Todo: проверка на валидность через IUniversityData
            subjects.Add(subject);
        }
        #endregion

        #region Private Methods

        #endregion

        #region Fields
        private List<string> subjects;
        private List<string> institutions;

        #endregion

    }
}
