using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_ProgrammingLang.ViewModels
{
    using Models;

    public class TaskViewModel
    {
        public List<Task>? Tasks { get; set; }
        public string? SearchTask { get; set; }
        public int LangID { get; set; }
    }
}
