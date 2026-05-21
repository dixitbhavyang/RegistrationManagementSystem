using Microsoft.EntityFrameworkCore;
using RegistrationManagementSystem.API.Models;

namespace RegistrationManagementSystem.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<UserDocument> Documents { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Registration>()
                .HasMany<UserDocument>(r => r.Documents)
                .WithOne(d => d.Registration)
                .HasForeignKey(d => d.RegistrationId);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Registration>()
                 .HasOne(r => r.State)
                 .WithMany()
                 .HasForeignKey(r => r.StateId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.City)
                .WithMany()
                .HasForeignKey(r => r.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(c => c.State)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.StateId);

            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "Gujarat" },
                new State { Id = 2, Name = "Maharashtra" },
                new State { Id = 3, Name = "Rajasthan" },
                new State { Id = 4, Name = "Delhi" },
                new State { Id = 5, Name = "Karnataka" }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Ahmedabad", StateId = 1 },
                new City { Id = 2, Name = "Surat", StateId = 1 },
                new City { Id = 3, Name = "Vadodara", StateId = 1 },
                new City { Id = 4, Name = "Rajkot", StateId = 1 },
                new City { Id = 5, Name = "Una", StateId = 1 },
                new City { Id = 6, Name = "Mumbai", StateId = 2 },
                new City { Id = 7, Name = "Pune", StateId = 2 },
                new City { Id = 8, Name = "Nagpur", StateId = 2 },
                new City { Id = 9, Name = "Jaipur", StateId = 3 },
                new City { Id = 10, Name = "Jodhpur", StateId = 3 },
                new City { Id = 11, Name = "New Delhi", StateId = 4 },
                new City { Id = 12, Name = "Dwarka", StateId = 4 },
                new City { Id = 13, Name = "Bangalore", StateId = 5 },
                new City { Id = 14, Name = "Mysore", StateId = 5 }
            );
        }
    }
}
