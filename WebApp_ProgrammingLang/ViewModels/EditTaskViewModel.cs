using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_ProgrammingLang.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditTaskViewModel
    {
        [Required(ErrorMessage = "Введите название задании")]
        [StringLength(100, ErrorMessage = "Превышено макс. кол-во символов")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Выберите язык программирование")]
        public int LanguageID { get; set; }

        public int UserID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? PublishDate { get; set; }

        [Required(ErrorMessage = "Укажите файл с описанием задании!")]
        public string? FileText { get; set; }

        [Required(ErrorMessage = "Укажите архив с проектом и тестом!")]
        public string? Filename { get; set; }
    }
}
