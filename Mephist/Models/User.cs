using Mephist.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mephist
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NickName { get; set; }
        public virtual List<Media> Photos { get; set; } = new List<Media>();
        public virtual List<Review> Reviews { get; set; } = new List<Review>();
    }
}