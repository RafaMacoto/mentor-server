using mentor.Models;
using Microsoft.EntityFrameworkCore;

namespace mentor.Data
{
    public class MentorDbContext : DbContext
    {

        public MentorDbContext(DbContextOptions<MentorDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Skill> Skills => Set<Skill>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().ToTable("TB_M_USER");
            modelBuilder.Entity<Skill>().ToTable("TB_M_SKILL");

            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Skills)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
