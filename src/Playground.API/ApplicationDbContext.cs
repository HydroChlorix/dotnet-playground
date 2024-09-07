using Microsoft.EntityFrameworkCore;

namespace Playground.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TimeZone> TimeZones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TimeZone>();
        }
    }

    public class TimeZone
    {
        public string Abbreviation { get; set; }
        public string TimeZoneName { get; set; }
    }
}


