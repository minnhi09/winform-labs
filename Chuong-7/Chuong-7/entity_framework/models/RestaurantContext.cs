using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace entity_framework.models
{
    public class RestaurantContext : DbContext
    {
        // DbSet properties cho các bảng
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAccount> RoleAccounts { get; set; }

        // Constructor mặc định
        public RestaurantContext()
        {
        }

        // Constructor với options
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        // Cấu hình connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Cấu hình connection string - có thể thay đổi theo môi trường
                optionsBuilder.UseSqlServer(
                    @"Server=localhost\MSSQLSERVER03;Database=chuong_7_entity_framework;Trusted_Connection=True;");
            }
        }

        // Cấu hình model và relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình composite key cho RoleAccount
            modelBuilder.Entity<RoleAccount>()
                .HasKey(ra => new { ra.AccountName, ra.RoleID });

            // Cấu hình relationship Restaurant -> Hall
            modelBuilder.Entity<Hall>()
                .HasOne(h => h.Restaurant)
                .WithMany(r => r.Halls)
                .HasForeignKey(h => h.RestaurantID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship Hall -> Table
            modelBuilder.Entity<Table>()
                .HasOne(t => t.Hall)
                .WithMany(h => h.Tables)
                .HasForeignKey(t => t.HallID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship FoodCategory -> Food
            modelBuilder.Entity<Food>()
                .HasOne(f => f.FoodCategory)
                .WithMany(fc => fc.Foods)
                .HasForeignKey(f => f.FoodCategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship Invoice -> Table
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Table)
                .WithMany(t => t.Invoices)
                .HasForeignKey(i => i.TableID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship Invoice -> Account
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Account)
                .WithMany(a => a.Invoices)
                .HasForeignKey(i => i.AccountID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship InvoiceDetails -> Invoice
            modelBuilder.Entity<InvoiceDetails>()
                .HasOne(id => id.Invoice)
                .WithMany(i => i.InvoiceDetails)
                .HasForeignKey(id => id.InvoiceID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship InvoiceDetails -> Food
            modelBuilder.Entity<InvoiceDetails>()
                .HasOne(id => id.Food)
                .WithMany(f => f.InvoiceDetails)
                .HasForeignKey(id => id.FoodID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship RoleAccount -> Account
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Account)
                .WithMany(a => a.RoleAccounts)
                .HasForeignKey(ra => ra.AccountName)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình relationship RoleAccount -> Role
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Role)
                .WithMany(r => r.RoleAccounts)
                .HasForeignKey(ra => ra.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình check constraints và default values
            modelBuilder.Entity<Table>()
                .Property(t => t.Status)
                .HasDefaultValue(0);

            modelBuilder.Entity<FoodCategory>()
                .Property(fc => fc.Type)
                .IsRequired();

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Status)
                .HasDefaultValue(false);

            modelBuilder.Entity<RoleAccount>()
                .Property(ra => ra.Actived)
                .HasDefaultValue(true);
        }
    }
}
