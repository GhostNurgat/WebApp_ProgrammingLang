namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Task
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public int LanguageID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DatePublish { get; set; }

        [Required]
        [StringLength(100)]
        public string FileText { get; set; }

        [Required]
        [StringLength(100)]
        public string FileTask { get; set; }

        public int ProgrammingLanguageID { get; set; }


        public virtual User User { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }


        public virtual List<TaskWork> Works { get; set; } = new List<TaskWork>();
    }
}
