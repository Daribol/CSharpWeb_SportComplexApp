using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportComplexApp.Data.Models;

namespace SportComplexApp.Data.Configuration
{
    public class DatabaseSeeder
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles =
            {
                "Admin",
                "Trainer",
                "Client"
            };

            foreach (var role in roles)
            {
                var roleExist = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                if(!roleExist)
                {
                    var result = roleManager.CreateAsync(new IdentityRole { Name = role}).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {role}");
                    }
                }
            }
        }

        public static void AssignAdminRole(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Client>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string adminEmail = configuration["AdminSettings:Username"];
            string adminPassword = configuration["AdminSettings:Password"];

            var existingAdmin = userManager.FindByEmailAsync(adminEmail!).GetAwaiter().GetResult();

            if (existingAdmin == null)
            {
                var adminUser = new Client
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var result = userManager.CreateAsync(adminUser, adminPassword!).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
