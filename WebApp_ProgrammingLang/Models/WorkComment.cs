namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class WorkComment
    {
        [Key]
        public int ID { get; set; }

        public int WorkID { get; set; }

        public int UserID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateComment { get; set; }

        public string? AnswerComment { get; set; }

        [Required]
        [StringLength(350)]
        public string CommentText { get; set; }

        [StringLength(100)]
        public string? Filename { get; set; }


        public virtual Work Work { get; set; }
        public virtual User User { get; set; }
    }
}
