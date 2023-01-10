using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<Inventory> Inventory { get; set; }//
        public DbSet<Moving> Moving { get; set; }//
        public DbSet<Orders> Orders { get; set; }//
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderType> OrderType { get; set; }
        public DbSet<RegistrationWrite> RegistrationWrite { get; set; }//
        public DbSet<RegistrationWriteType> RegistrationWriteType { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Warehouses> Warehouses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus[] {
                new OrderStatus{Id = 1, Name="Создан"},
                new OrderStatus{Id = 2, Name="Выполнен"},
            });
            builder.Entity<OrderType>().HasData(new OrderType[] {
                new OrderType{Id = 1,Name="Заказ клиента"},
                new OrderType{Id = 2, Name="Заказ поставщику"},
            });
            builder.Entity<RegistrationWriteType>().HasData(new RegistrationWriteType[] {
                new RegistrationWriteType{Id = 1,Name="Приход"},
                new RegistrationWriteType{Id = 2, Name="Расход"},
            });
        }
        //public async Task<int> SaveChangesAsync()
        //{
        //    return await base.SaveChangesAsync();
        //}
    }
}
