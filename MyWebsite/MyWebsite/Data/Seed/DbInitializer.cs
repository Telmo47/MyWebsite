using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Models;

namespace MyWebsite.Data.Seed
{
    internal class DbInitializer
    {
        internal static async Task InitializeAsync(AppDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            // Ensure database exists
            await dbContext.Database.MigrateAsync();

            bool wasAdds = false;

            // 1. Create the "Admin" role (capital A) if it does not exist
            const string adminRoleName = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
                wasAdds = true;
            }

            // 2. Create the admin user if it does not exist
            var adminEmail = "admin@mail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Aa0_aa");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                    wasAdds = true;
                }
            }
            else
            {
                // Ensure existing user has the "Admin" role
                if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                    wasAdds = true;
                }
            }

            // 3. Seed Projects table if empty
            if (!dbContext.Projects.Any())
            {
                var project = new Projects
                {
                    Title = "GGData",
                    Description = "My first Website ever developed, quite simple, a display of critics where one can criticise any game that the admin gets in the website"
                };

                await dbContext.Projects.AddAsync(project);
                wasAdds = true;
            }

            if (wasAdds)
            {
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
