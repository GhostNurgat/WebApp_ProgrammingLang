using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_ProgrammingLang.ViewModels
{
    using Models;

    public class LanguagesViewModel
    {
        public List<ProgrammingLanguage>? Languages { get; set; }
        public SelectList? Types { get; set; }
        public string? SearchLanguage { get; set; }
        public string? StringType { get; set; }
    }
}
