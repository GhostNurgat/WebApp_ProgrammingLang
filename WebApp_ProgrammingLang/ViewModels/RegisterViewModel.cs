namespace WebApp_ProgrammingLang.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [Display(Name = "Имя пользователя")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email обязательно")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Не соотвествует адресу электронной почты!")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязательно")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль не должен содержать не более 6 символов")]
        [MaxLength(20, ErrorMessage = "Пароль не должен превышать не больше 20 символов")]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Пожалуйста, подтвердите пароль!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string? PasswordConfirm { get; set; }
    }
}
