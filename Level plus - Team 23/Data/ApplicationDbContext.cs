using Level_plus___Team_23.Models;
using Level_plus___Team_23.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Level_plus___Team_23.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        
                public virtual DbSet<ApplicationUser> users { get; set; }
                public virtual DbSet<Level_plus___Team_23.Models.Course> courses { get; set; }

                public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
                public virtual DbSet<CourseInShoppingCart> CourseInShoppingCarts { get; set; }
                public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        public virtual DbSet<FavouriteList> FavouriteLists { get; set; }

        public virtual DbSet<PurchasedCourse> PurchasedCourses { get; set; }

        public DbSet<WatchLater> WatchLaterCourses { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
                {
                    base.OnModelCreating(builder);

                    builder.Entity<Course>()
                        .Property(z => z.Id)
                        .ValueGeneratedOnAdd();

                    builder.Entity<ShoppingCart>()
                        .Property(z => z.Id)
                        .ValueGeneratedOnAdd();

                    builder.Entity<CourseInShoppingCart>()
                        .Property(z => z.Id)
                        .ValueGeneratedOnAdd();

                    builder.Entity<CourseInShoppingCart>()
                        .HasOne(z => z.CurrentCourse)
                        .WithMany(z => z.CourseInShoppingCarts)
                        .HasForeignKey(z => z.CourseId);

                    builder.Entity<CourseInShoppingCart>()
                        .HasOne(z => z.UserCart)
                        .WithMany(z => z.CourseInShoppingCarts)
                        .HasForeignKey(z => z.ShoppingCartId);

                    builder.Entity<ShoppingCart>()
                        .HasOne<ApplicationUser>(z => z.Owner)
                        .WithOne(z => z.UserCart)
                        .HasForeignKey<ShoppingCart>(z => z.OwnerId);

                    builder.Entity<CourseInOrder>()
                        .Property(z => z.Id)
                        .ValueGeneratedOnAdd();

                    builder.Entity<CourseInOrder>()
                        .HasOne(z => z.Course)
                        .WithMany(z => z.CourseInOrders)
                        .HasForeignKey(z => z.CourseId);

                    builder.Entity<CourseInOrder>()
                        .HasOne(z => z.Order)
                        .WithMany(z => z.CourseInOrders)
                        .HasForeignKey(z => z.OrderId);
                }
        
    }

}
