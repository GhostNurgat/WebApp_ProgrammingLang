namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Work
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int UserID { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Code { get; set; }

        public virtual User User { get; set; }


        public virtual List<WorkComment> Comments { get; set; } = new List<WorkComment>();
        public virtual List<WorkLike> WorkLikes { get; set; } = new List<WorkLike>();
    }
}
