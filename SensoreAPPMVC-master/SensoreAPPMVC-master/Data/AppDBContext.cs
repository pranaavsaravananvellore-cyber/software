using Microsoft.EntityFrameworkCore;
using SensoreAPPMVC.Models;

namespace SensoreAPPMVC.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }

        // ADD THESE ↓↓↓
        public DbSet<Heatmap> Heatmaps { get; set; }
        public DbSet<HeatmapValue> HeatmapValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("User")
                .HasValue<Patient>("Patient");

            // Heatmap → HeatmapValues (1→many)
            modelBuilder.Entity<Heatmap>()
                .HasMany(h => h.Values)
                .WithOne()
                .HasForeignKey(v => v.HeatmapId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
