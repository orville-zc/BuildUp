using Microsoft.EntityFrameworkCore;

namespace BuildUp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Action> Actions { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Action>()
                .HasMany<TimeEntry>(b => b.TimeEntries)
                .WithOne(t => t.Action)
                .HasForeignKey(t => t.ActionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
