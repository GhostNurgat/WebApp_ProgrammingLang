namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class Work
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        public int UserID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Code { get; set; }

        public int LanguageID { get; set; }

        public int ProgrammingLanguageID { get; set; }

        [NotMapped]
        public SelectList Languages { get; set; }


        [ForeignKey("UserID")]
        public virtual User? User { get; set; }

        [ForeignKey("ProgrammingLanguageID")]
        public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }


        public virtual List<WorkComment> Comments { get; set; } = new List<WorkComment>();
        public virtual List<WorkLike> WorkLikes { get; set; } = new List<WorkLike>();
    }
}
