using Booking.Domain.Entities;
using Booking.Domain.Entities.HotelAggregate;
using Booking.Domain.Entities.OrderAggregate;
using Booking.Domain.Entities.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,int,
        IdentityUserClaim<int> , AppUserRole,IdentityUserLogin<int>,
        IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Hotel>? Hotels { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<HotelImage>? HotelImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Add Hotels Table
            modelBuilder.Entity<Hotel>().ToTable("Hotels");
            modelBuilder.Entity<Hotel>().HasKey(i => i.Id);
            //Add Rooms Table
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Room>().HasKey(i => i.Id);
            modelBuilder.Entity<Room>().Property(i => i.BedType).HasConversion<string>();

            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.Entity<HotelImage>().HasKey(i => i.Id);
            modelBuilder.Entity<HotelImage>().ToTable("HotelImages");

            //Create One to Many connections
            modelBuilder.Entity<Hotel>()
                .HasMany(i => i.Rooms)
                .WithOne(i => i.Hotel)
                .HasForeignKey(i => i.HotelId);
            modelBuilder.Entity<Room>()
                .HasMany(i => i.Orders)
                .WithOne(i => i.Room)
                .HasForeignKey(i => i.RoomId);
            modelBuilder.Entity<Hotel>()
                .HasMany(i => i.NumberOfRoomsByTypes)
                .WithOne(i => i.Hotel)
                .HasForeignKey(i => i.HotelId);
            modelBuilder.Entity<AppUser>()
                .HasMany(i => i.AppUserRoles)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);
            modelBuilder.Entity<AppRole>()
                .HasMany(i => i.AppUserRoles)
                .WithOne(i => i.Role)
                .HasForeignKey(i => i.RoleId);
            modelBuilder.Entity<AppUser>()
                .HasMany(i => i.Hotels)
                .WithMany(i => i.Users);

            modelBuilder.Entity<AppUser>()
                .HasMany(i => i.Orders)
                .WithOne(i => i.AppUser)
                .HasForeignKey(I => I.UserId);

            modelBuilder.Entity<Hotel>()
                .HasMany(i => i.HotelImages)
                .WithOne(i => i.Hotel)
                .HasForeignKey(i => i.HotelId);

        }

        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=chiko\\SQLEXPRESS;Database=BookingDB;Trusted_Connection=True;MultipleActiveResultSets=True");
                return new ApplicationDbContext(options.Options);
            }
        }


    }
}
