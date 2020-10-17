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
        public virtual EducationalMaterial EducationalMaterial { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string MediaName { get; set; }
        public string PartialMediaPath { get; set; }
       // public string MediaMimeType { get; set; }
        public int? UserId { get; set; }
        public virtual  User User { get; set; }
        public DateTime CreatedDate { get; set; }

        public static string DefaultAvatarPath = @"Shared/DefaultAvatar.jpg";
        

        public Media()
        {
        }

        public string GetPath()
        {
            return PartialMediaPath + "/" + MediaName;
        }
    }
}
