using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PackBuddy.Controllers;
using PackBuddy.Models;

namespace PackBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Gear> Gears { get; set; }
        public DbSet<GearTrip> GearTrips { get; set; }
        public DbSet<GearType> GearTypes { get; set; }
        public DbSet<SharedGear> SharedGears { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed new user
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "James",
                LastName = "Nitz",
                UserName = "james@james.com",
                NormalizedUserName = "JAMES@JAMES.COM",
                Email = "james@james.com",
                NormalizedEmail = "JAMES@JAMES.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Create  types
            modelBuilder.Entity<GearType>().HasData(
                new GearType()
                {
                    Id = 1,
                    Label = "Clothing"
                },
                new GearType()
                {
                    Id = 2,
                    Label = "Cooking"
                },
                new GearType()
                {
                    Id = 3,
                    Label = "Backpacks"
                },
                new GearType()
                {
                    Id = 4,
                    Label = "Tents"
                },
                new GearType()
                {
                    Id = 5,
                    Label = "Sleeping Gear"
                },
                new GearType()
                {
                    Id = 6,
                    Label = "Accessories"
                }
            );

            // seed Gear   
            modelBuilder.Entity<Gear>().HasData(
                new Gear()
                {
                    Id = 1,
                    ApplicationuserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    Condtion = "Like New",
                    Description = "Light, durable, water-proof hiking Boots",
                    Name = "Terrex Free Hiker Pro",
                    Rating = 5,
                    GearTypeId = 1
                },
                new Gear()
                {
                    Id = 2,
                    ApplicationuserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    Condtion = "Slightly used",
                    Description = "Solid tent",
                    Name = "Big Agnes UL2",
                    Rating = 4,
                    GearTypeId = 4
                }
            );
        }
    }
}
    