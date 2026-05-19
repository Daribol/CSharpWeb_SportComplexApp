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

        public static void SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Client>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string adminEmail = configuration["AdminSettings:Username"] ?? "admin@sport.com";
            string adminPassword = configuration["AdminSettings:Password"] ?? "Admin123!";
            CreateUserWithRole(userManager, adminEmail, adminPassword, "Admin", "Admin", "User");

            CreateUserWithRole(userManager, "trainer@sport.com", "Trainer123!", "Trainer", "Ivan", "Ivanov");

            CreateUserWithRole(userManager, "client@sport.com", "Client123!", "Client", "Petar", "Petrov");
        }

        private static void CreateUserWithRole(UserManager<Client> userManager, string email, string password, string role, string firstName, string lastName)
        {
            if (userManager.FindByEmailAsync(email).GetAwaiter().GetResult() == null)
            {
                var user = new Client
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
                }
            }
        }
    }
}
