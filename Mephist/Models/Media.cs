using Mephist.Models.Intefaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Models
{
    public class Media : IUploadInfo
    {
        [Key]
        public int Id { get; set; }
        public int? EducationalMaterialId { get; set; }
        public string MediaName { get; set; }
        [Column(TypeName = "varbinary(maxа)")]
        public byte[] MediaFile { get; set; }
        public string MediaMimeType { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
