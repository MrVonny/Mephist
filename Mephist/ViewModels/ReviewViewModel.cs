using Mephist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        [MaxLength(4000)]
        public string Text { get; set; }
        public bool Anonymously { get; set; }
        public int EmployeeId { get; set; }
    }
}
