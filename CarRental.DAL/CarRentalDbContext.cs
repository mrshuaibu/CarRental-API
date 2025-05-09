using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Models
{
    public class CarRentalDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Booking Entity
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.BookingId);

            modelBuilder.Entity<Booking>()
                .Property(b => b.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.CarId)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.StartDate)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.EndDate)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(50);

            // Car Entity
            modelBuilder.Entity<Car>()
                .HasKey(c => c.CarId);

            modelBuilder.Entity<Car>()
                .Property(c => c.Make)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(c => c.Year)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Car>()
                .Property(c => c.RentalPricePerDay)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Car>()
                .HasIndex(c => c.LicensePlate)
                .IsUnique();

            // Customer Entity
            modelBuilder.Entity<Customer>()
                .HasKey(cu => cu.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(cu => cu.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(cu => cu.Email)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(cu => cu.PhoneNumber)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(cu => cu.DriverLicense)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(cu => cu.Bookings)
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany()
                .HasForeignKey(b => b.CarId);
        }
    }
}
