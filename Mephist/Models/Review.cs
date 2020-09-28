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
        [StringLength(10000)]
        [Column(TypeName = "nvarchar(10000)")]
        public string Text { get; set; }
        public bool Anonymously { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}