using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IPasswordHasher<IdentityUser> passwordHasher;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedRoles(builder);
            this.SeedUsers(builder);
            this.SeedUserRoles(builder);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasData(
                new AppRole() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole() { Id = 2, Name = "Member", NormalizedName = "MEMBER" }
                );
        }
        private void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1, // primary key
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "P@$$w0rd")
                }
            );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<AppUserRole>().HasData(
            new AppUserRole
            {
                RoleId = 1,
                UserId = 1
            }
        );
        }


        public DbSet<Resource> Resources { get; set; }

    }
}
