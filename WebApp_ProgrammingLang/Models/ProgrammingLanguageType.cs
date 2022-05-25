namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProgrammingLanguageType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }


        public virtual List<ProgrammingLanguage> ProgrammingLanguages { get; set; } = new List<ProgrammingLanguage>();
    }
}
