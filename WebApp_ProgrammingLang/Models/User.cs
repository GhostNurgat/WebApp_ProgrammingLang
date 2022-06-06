namespace WebApp_ProgrammingLang.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser<int>
    {
        [StringLength(50)]
        public string? Surname { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string? About { get; set; }

        [StringLength(100)]
        public string? UserImage { get; set; }

        public string? Languages { get; set; }


        public virtual List<Work> Works { get; set; } = new List<Work>();
        public virtual List<Task> Tasks { get; set; } = new List<Task>();
        public virtual List<TaskWork> TaskWorks { get; set; } = new List<TaskWork>();
        public virtual List<WorkLike> WorkLikes { get; set; } = new List<WorkLike>();
        public virtual List<WorkComment> WorkComments { get; set; } = new List<WorkComment>();
        public virtual List<MentorBid> MentorBids { get; set; } = new List<MentorBid>();
    }
}
