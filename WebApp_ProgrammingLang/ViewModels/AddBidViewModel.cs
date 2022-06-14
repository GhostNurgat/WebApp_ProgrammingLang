namespace WebApp_ProgrammingLang.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddBidViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Укажите языки программирование")]
        public string? Language { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        public string? Description { get; set; }
    }
}
