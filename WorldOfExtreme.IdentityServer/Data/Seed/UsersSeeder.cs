
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorldOfExtreme.IdentityServer.Model;
using WorldOfExtreme.Infrastructure.Interface;

namespace WorldOfExtreme.IdentityServer.Data.Seed
{
    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(DbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var db = dbContext as ApplicationDbContext;

            if (!db.Users.Any())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com"
                };

                var user = new ApplicationUser
                {
                    UserName = "user",
                    Email = "user@user.com"

                };

                await userManager.CreateAsync(adminUser, "admin123");
                await userManager.CreateAsync(user, "user123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}
