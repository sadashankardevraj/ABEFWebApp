using ABEFWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ABEFWebApp.Services;

public static class IdentityDataSeeder
{
    public static readonly string[] Roles = ["Admin", "Member", "Evaluator"];

    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("IdentityDataSeeder");

        foreach (var roleName in Roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    logger.LogError("Failed to create role {Role}: {Errors}",
                        roleName, string.Join("; ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        const string adminEmail = "admin@bea.test";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                Organisation = "Business Excellence Australia"
            };

            var createResult = await userManager.CreateAsync(admin, "Admin@12345");
            if (!createResult.Succeeded)
            {
                logger.LogError("Failed to create admin user: {Errors}",
                    string.Join("; ", createResult.Errors.Select(e => e.Description)));
                return;
            }
        }

        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        const string evaluatorEmail = "evaluator@bea.test";
        var evaluator = await userManager.FindByEmailAsync(evaluatorEmail);
        if (evaluator is null)
        {
            evaluator = new ApplicationUser
            {
                UserName = evaluatorEmail,
                Email = evaluatorEmail,
                EmailConfirmed = true,
                FirstName = "Evaluator",
                LastName = "User",
                Organisation = "Business Excellence Australia"
            };

            var createResult = await userManager.CreateAsync(evaluator, "Evaluator@12345");
            if (!createResult.Succeeded)
            {
                logger.LogError("Failed to create evaluator user: {Errors}",
                    string.Join("; ", createResult.Errors.Select(e => e.Description)));
                return;
            }
        }

        if (!await userManager.IsInRoleAsync(evaluator, "Evaluator"))
        {
            await userManager.AddToRoleAsync(evaluator, "Evaluator");
        }
    }
}
