using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp_ProgrammingLang.ViewModels
{
    using Models;

    public class WorksViewModel
    {
        public List<Work>? Works { get; set; }
        public SelectList? Languages { get; set; }
        public string? Language { get; set; }
        public string? SeacrhWork { get; set; }
    }
}
