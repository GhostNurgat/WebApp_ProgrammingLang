namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TaskWork
    {
        [Key]
        public int ID { get; set; }

        public int TaskID { get; set; }

        public int UserID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateLoad { get; set; }

        [StringLength(100)]
        public string? Filename { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public string? Message { get; set; }


        public virtual User User { get; set; }
        public virtual Task Task { get; set; }


        public virtual List<TaskWorkMessage> Messages { get; set; } = new List<TaskWorkMessage>();
    }
}
