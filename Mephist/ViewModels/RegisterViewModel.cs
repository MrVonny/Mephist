using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.ViewModels
{
    public class RegisterViewModel
    {


        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Введите логин")]
        [MaxLength(30, ErrorMessage = "Максимальная длина 30 символов")]
        [MinLength(5, ErrorMessage = "Минимальная длина 5 символов")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Введите e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}
