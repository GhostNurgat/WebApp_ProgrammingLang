namespace WebApp_ProgrammingLang.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WorkLike
    {
        [Key, Column(Order = 0)]
        public int WorkID { get; set; }

        [Key, Column(Order = 1)]
        public int UserID { get; set; }

        
        public virtual Work? Work { get; set; }
        public virtual User? User { get; set; }
    }
}
