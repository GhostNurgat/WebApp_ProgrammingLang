namespace WebApp_ProgrammingLang.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ProgLangContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ProgLangContext(DbContextOptions<ProgLangContext> options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WorkLike>().HasKey(wl => new { wl.WorkID, wl.UserID });
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskWork> TaskWorks { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<ProgrammingLanguageType> ProgrammingLanguageTypes { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkComment> WorkComments { get; set; }
        public DbSet<WorkLike> WorkLikes { get; set; }
    }
}
