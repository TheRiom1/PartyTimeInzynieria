
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Party_Feature>().HasKey(am => new
            {
                am.PartyId,
                am.FeatureId
            });

            modelBuilder.Entity<Party_Feature>().HasOne(m => m.Party).WithMany(am => am.Party_Feature).HasForeignKey(m => m.PartyId);
            modelBuilder.Entity<Party_Feature>().HasOne(m => m.Feature).WithMany(am => am.Party_Feature).HasForeignKey(m => m.FeatureId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Party_Feature> Party_Feature { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Organizator> Organizators { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Product> Products { get; set; }

        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
