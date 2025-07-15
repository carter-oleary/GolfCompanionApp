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

        private User GetUser()
        {
            return new User
            {
                UserId = 1, // Default user for testing purposes
                Email = "john_doe@fake.email",
                FirstName = "John",
                LastName = "Doe",
                Handicap = 15
            };
        }

        private Club[] GetClubs()
        {
            return new Club[]
            {
                new Club { ClubId = 1, UserId = 1, ClubName = "Dr", ClubDistance = 270 },
                new Club { ClubId = 2, UserId = 1, ClubName = "3w", ClubDistance = 240 },
                new Club { ClubId = 3, UserId = 1, ClubName = "4h", ClubDistance = 220 },
                new Club { ClubId = 4, UserId = 1, ClubName = "5i", ClubDistance = 205 },
                new Club { ClubId = 5, UserId = 1, ClubName = "6i", ClubDistance = 195 },
                new Club { ClubId = 6, UserId = 1, ClubName = "7i", ClubDistance = 180 },
                new Club { ClubId = 7, UserId = 1, ClubName = "8i", ClubDistance = 165 },
                new Club { ClubId = 8, UserId = 1, ClubName = "9i", ClubDistance = 150 },
                new Club { ClubId = 9, UserId = 1, ClubName = "Pw", ClubDistance = 135 },
                new Club { ClubId = 10, UserId = 1, ClubName = "Gw", ClubDistance = 125 },
                new Club { ClubId = 11, UserId = 1, ClubName = "Aw", ClubDistance = 115 },
                new Club { ClubId = 12, UserId = 1, ClubName = "Sw", ClubDistance = 105 },
                new Club { ClubId = 13, UserId = 1, ClubName = "Lw", ClubDistance = 95 },
                new Club { ClubId = 14, UserId = 1, ClubName = "Pu", ClubDistance = 0 }
            };
        }

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

            modelBuilder.Entity<Shot>()
                .Property(s => s.Result)
                .HasConversion<string>();

            modelBuilder.Entity<Shot>()
                .Property(s => s.Lie)
                .HasConversion<string>();

            // Seed initial data
            modelBuilder.Entity<User>().HasData(GetUser());
            modelBuilder.Entity<Club>().HasData(GetClubs());

            base.OnModelCreating(modelBuilder);


        }
    }
} 