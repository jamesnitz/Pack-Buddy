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
                    Label = "Clothing",
                    ImagePath = "type_Clothes.png"
                },
                new GearType()
                {
                    Id = 2,
                    Label = "Cookware",
                    ImagePath = "type_cooking.png"
                },
                new GearType()
                {
                    Id = 3,
                    Label = "Backpacks",
                    ImagePath = "type_backpack.png"
                },
                new GearType()
                {
                    Id = 4,
                    Label = "Hygiene",
                    ImagePath = "type_hygiene.png"
                },
                new GearType()
                {
                    Id = 5,
                    Label = "Sleep System",
                    ImagePath = "type_sleeping.png"
                },
                new GearType()
                {
                    Id = 6,
                    Label = "Activities",
                    ImagePath = "type_activities.png"
                },
                new GearType()
                {
                    Id = 7,
                    Label = "Electronics",
                    ImagePath = "type_electronics.png"
                },
                new GearType()
                {
                    Id = 8,
                    Label = "Tools/misc.",
                    ImagePath = "type_tools.png"
                }
            );

            // seed Gear   
            modelBuilder.Entity<Gear>().HasData(
                new Gear()
                {
                    Id = 1,
                    ApplicationuserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    Condition = "Like New",
                    Description = "Light, durable, water-proof hiking Boots",
                    Name = "Terrex Free Hiker Pro",
                    Rating = 5,
                    ImagePath = null,
                    GearTypeId = 1
                },
                new Gear()
                {
                    Id = 2,
                    ApplicationuserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    Condition = "Slightly used",
                    Description = "Solid tent",
                    Name = "Big Agnes UL2",
                    Rating = 4,
                    ImagePath = null,
                    GearTypeId = 5
                }
            );
        }
    }
}
    