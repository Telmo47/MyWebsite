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
            // 1. Makes sure the provided services are not null
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            // 2. Creates the database if it does not exist
            await dbContext.Database.MigrateAsync();


            bool wasAdds = false;

            // 3. Creation of the "admin" role if it does not exist
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
                wasAdds = true;
            }

            // 4. Creation of the admin user if it does not exist
            var adminUser = await userManager.FindByEmailAsync("admin@mail.com");
            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Aa0_aa");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                    wasAdds = true;
                }
            }

            // 5. Seeding the Projects table with initial data if it is empty
            if (!dbContext.Projects.Any())
            {
                var project = new Projects
                {
                    Title = "GGData",
                    Description = "My first Website ever developed, quite simple, a display of critics where one can criticise any game taht the admin gets in the website"
                };

                await dbContext.Projects.AddAsync(project);
                wasAdds = true;
            }

            // 6. Saves changes to the database if there were any additions
            if (wasAdds)
            {
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
