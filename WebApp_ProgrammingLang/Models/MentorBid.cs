namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MentorBid
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string? Languages { get; set; }

        [Required]
        public string? Description { get; set; }


        public virtual User User { get; set; }
    }
}
