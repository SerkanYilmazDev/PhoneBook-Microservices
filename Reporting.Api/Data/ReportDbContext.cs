using Microsoft.EntityFrameworkCore;
using Reporting.Api.Data.Entity;

namespace Reporting.Api.Data
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
        }

        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("report");
            modelBuilder.Entity<Report>().ToTable("reports");
            modelBuilder.Entity<Report>().HasMany(x => x.Items);
            modelBuilder.Entity<Report>().Property(s => s.Status).HasConversion<string>();
            modelBuilder.Entity<ReportItem>().ToTable("report_items");
        }
    }
}
