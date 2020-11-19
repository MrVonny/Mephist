using Mephist.Models.Intefaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{

    public class Media : IUploadInfo
    {
        [Key]
        public int Id { get; set; }
        public int? EducationalMaterialId { get; set; }
        [Required]
        public string ContentType { get; set; }
        public virtual EducationalMaterial EducationalMaterial { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [Required]
        public string MediaName { get; set; }
        [Required]
        public string PartialMediaPath { get; set; }
        public string UserId { get; set; }
        public virtual  User User { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public static string DefaultAvatarPath = @"Shared/DefaultAvatar.jpg";
        

        public Media()
        {
        }

        public Media(EducationalMaterial educationalMaterial, string mediaName,string contentType, string partialMediaPath, User user)
        {
            EducationalMaterial = educationalMaterial;
            ContentType = contentType;
            MediaName = mediaName;
            PartialMediaPath = partialMediaPath;
            User = user;
            CreatedDate = DateTime.Now;
        }

        public Media(Employee employee, string mediaName, string contentType, string partialMediaPath, User user)
        {
            Employee = employee;
            ContentType = contentType;
            MediaName = mediaName;
            PartialMediaPath = partialMediaPath;
            User = user;
            CreatedDate = DateTime.Now;
        }


        public string GetPath()
        {
            return PartialMediaPath + "/" + MediaName;
        }
    }
}
