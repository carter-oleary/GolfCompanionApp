using Microsoft.EntityFrameworkCore;
using GolfCompanion.Models;

namespace GolfCompanion.Data
{
    public class GolfDbContext : DbContext
    {
        public GolfDbContext(DbContextOptions<GolfDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tee> Tees { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Hole> Holes { get; set; }
        public DbSet<Shot> Shots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configure the database connection string here if not set externally
                optionsBuilder.UseSqlite("Data Source=golfcompanion.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<Club>()
                .HasOne(c => c.User)
                .WithMany(u => u.Clubs)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tee>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Tees)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Round>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rounds)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Round>()
                .HasOne(r => r.Tee)
                .WithMany(t => t.Rounds)
                .HasForeignKey(r => r.TeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hole>()
                .HasOne(h => h.Tee)
                .WithMany(t => t.Holes)
                .HasForeignKey(h => h.TeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shot>()
                .HasOne(s => s.Hole)
                .WithMany(h => h.Shots)
                .HasForeignKey(s => s.HoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shot>()
                .HasOne(s => s.Round)
                .WithMany(r => r.Shots)
                .HasForeignKey(s => s.RoundId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shot>()
                .HasOne(s => s.Club)
                .WithMany(c => c.Shots)
                .HasForeignKey(s => s.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure enum conversion
            modelBuilder.Entity<Shot>()
                .Property(s => s.ShotType)
                .HasConversion<string>();
        }
    }
} 