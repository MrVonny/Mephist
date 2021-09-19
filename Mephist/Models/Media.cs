using Mephist.Models.Intefaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Services;

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
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Key { get; set; }

        public static readonly string DefaultAvatarPath = @"Content/Shared/DefaultAvatar.jpg";
        

        public Media()
        {
        }

        public Media(EducationalMaterial educationalMaterial, string mediaName, string key, string contentType, User user)
        {
            EducationalMaterial = educationalMaterial;
            ContentType = contentType;
            MediaName = mediaName;
            User = user;
            CreatedDate = DateTime.Now;
            Key = key;
        }

        public Media(Employee employee, string mediaName, string key, string contentType, User user)
        {
            Employee = employee;
            ContentType = contentType;
            MediaName = mediaName;
            User = user;
            CreatedDate = DateTime.Now;
            Key = key;
        }

        public string GetSignerUrl()
        {
            var storage = new AwsS3Storage();
            return storage.GetPreSignedUrl(this);
            
        }
    }
}
