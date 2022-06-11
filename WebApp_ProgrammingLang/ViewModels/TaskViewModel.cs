using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_ProgrammingLang.ViewModels
{
    using Models;

    public class TaskViewModel
    {
        public List<Task>? Tasks { get; set; }
        public SelectList? Languages { get; set; }
        public string? Language { get; set; }
        public string? SearchTask { get; set; }
        public int LangID { get; set; }
    }
}
