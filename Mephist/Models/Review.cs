using Mephist.Models.Intefaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mephist.Models
{
    public class Review : IUploadInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(4000)]
        [Column(TypeName = "nvarchar(4000)")]
        public string Text { get; set; }
        [Required]
        public bool Anonymously { get; set; }
        [Range(0, 10)]
        public int? Score {get;set;}
        public virtual int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public virtual DateTime CreatedDate { get; set; }

    }
}