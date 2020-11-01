using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.ViewModels
{
    public class RegisterViewModel
    {


        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
