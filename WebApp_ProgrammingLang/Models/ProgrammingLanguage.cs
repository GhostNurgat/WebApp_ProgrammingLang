namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProgrammingLanguage
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(20)]
        public string Platform { get; set; }

        [Required]
        public int TypeID { get; set; }

        [StringLength(100)]
        public string? Logo { get; set; }


        public ProgrammingLanguageType Type { get; set; }

        public virtual List<Task> Tasks { get; set; }
    }
}
