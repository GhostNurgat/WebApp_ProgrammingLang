using System.ComponentModel.DataAnnotations;

namespace WebApp_ProgrammingLang.ViewModels
{
    public class EditUserViewModel
    {
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательно!")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Имя обязательно!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательно!")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string? About { get; set; }
        public string? UserImage { get; set; }
    }
}
