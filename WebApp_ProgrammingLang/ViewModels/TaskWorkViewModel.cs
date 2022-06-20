using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_ProgrammingLang.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class TaskWorkViewModel
    {
        public int TaskID { get; set; }

        public int UserID { get; set; }

        public DateTime DateLoad { get; set; }

        public string? Filename { get; set; }

        public bool IsComplited { get; set; }

        public string? Message { get; set; }
    }
}
