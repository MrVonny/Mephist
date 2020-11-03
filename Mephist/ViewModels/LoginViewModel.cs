using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Введите email.")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль.")]
        public string Password { get; set; }

        [Display(Name = "Запонмить меня")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
