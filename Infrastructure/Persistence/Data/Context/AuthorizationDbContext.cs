using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Context
{
    public class AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options, IConfiguration configuration) : IdentityDbContext<ApplicationUser>(options)
    {

     
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Ignore<IdentityUserRole<string>>();
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();


            builder.Entity<IdentityRole>().HasData(new IdentityRole {Id= "b8c10d5f-8671-469c-9dc8-7a31b415e607",
                Name = "Admin" },
                new IdentityRole {Id= "f9298399-30e9-41ea-b299-10135811f0ba",
                    Name = "Customer" },
                new IdentityRole {Id= "273ee1bf-4213-4ced-a4cf-25d6efff0343",
                    Name = "Support Agent" });

            var hasher = new PasswordHasher<ApplicationUser>();
            var Hash = hasher.HashPassword(null, configuration["SeedAdmin:Password"]);
            var User = new ApplicationUser()
            {
                Id = "213566ac-e6de-4571-a2e2-1ec9b08f8aba",
                Email = configuration["SeedAdmin:Email"],
                FirstName = configuration["SeedAdmin:FirstName"]!,
                LastName = configuration["SeedAdmin:LastName"]!,
                PasswordHash = Hash,
                RoleId= "b8c10d5f-8671-469c-9dc8-7a31b415e607"


            };
            builder.Entity<ApplicationUser>().HasData(User);

        }
    
    }
}
