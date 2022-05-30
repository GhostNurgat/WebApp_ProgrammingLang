namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TaskWorkMessage
    {
        [Key]
        public int TaskWorkID { get; set; }

        public string? Message { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateMessage { get; set; }

        [StringLength(100)]
        public string? Filename { get; set; }


        public virtual TaskWork TaskWork { get; set; }
    }
}
