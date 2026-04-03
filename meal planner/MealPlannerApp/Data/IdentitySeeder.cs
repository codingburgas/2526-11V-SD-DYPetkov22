using MealPlannerApp.Infrastructure;
using MealPlannerApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Data;

public static class IdentitySeeder
{
    private const string DefaultAdminUserName = "admin";
    private const string DefaultAdminEmail = "admin@mealplanner.local";
    private const string DefaultAdminPassword = "Admin123!";

    public static async Task SeedAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        foreach (var roleName in ApplicationRoles.All)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                continue;
            }

            var createRoleResult = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            EnsureSuccess(createRoleResult, $"creating role '{roleName}'");
        }

        await EnsureAdminAccountAsync(userManager, configuration);
        await SyncIdentityRolesAsync(userManager);
    }

    private static async Task EnsureAdminAccountAsync(UserManager<User> userManager, IConfiguration configuration)
    {
        var adminPassword = configuration["IdentitySeed:AdminPassword"];
        if (string.IsNullOrWhiteSpace(adminPassword))
        {
            adminPassword = DefaultAdminPassword;
        }

        var adminUser = await userManager.FindByNameAsync(DefaultAdminUserName)
            ?? await userManager.FindByEmailAsync(DefaultAdminEmail);

        if (adminUser is null)
        {
            adminUser = new User
            {
                UserName = DefaultAdminUserName,
                Email = DefaultAdminEmail,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                Role = UserRole.Admin
            };

            var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
            EnsureSuccess(createUserResult, "creating the seeded admin account");
            return;
        }

        var requiresUpdate = false;

        if (!string.Equals(adminUser.UserName, DefaultAdminUserName, StringComparison.Ordinal))
        {
            adminUser.UserName = DefaultAdminUserName;
            requiresUpdate = true;
        }

        if (!string.Equals(adminUser.Email, DefaultAdminEmail, StringComparison.OrdinalIgnoreCase))
        {
            adminUser.Email = DefaultAdminEmail;
            requiresUpdate = true;
        }

        if (!adminUser.EmailConfirmed)
        {
            adminUser.EmailConfirmed = true;
            requiresUpdate = true;
        }

        if (adminUser.CreatedAt == default)
        {
            adminUser.CreatedAt = DateTime.UtcNow;
            requiresUpdate = true;
        }

        if (adminUser.Role != UserRole.Admin)
        {
            adminUser.Role = UserRole.Admin;
            requiresUpdate = true;
        }

        if (requiresUpdate)
        {
            var updateUserResult = await userManager.UpdateAsync(adminUser);
            EnsureSuccess(updateUserResult, "updating the seeded admin account");
        }

        if (!await userManager.HasPasswordAsync(adminUser))
        {
            var addPasswordResult = await userManager.AddPasswordAsync(adminUser, adminPassword);
            EnsureSuccess(addPasswordResult, "setting the seeded admin password");
        }
    }

    private static async Task SyncIdentityRolesAsync(UserManager<User> userManager)
    {
        var users = await userManager.Users.ToListAsync();

        foreach (var user in users)
        {
            var desiredRole = user.Role == UserRole.Admin
                ? ApplicationRoles.Admin
                : ApplicationRoles.User;

            var currentRoles = await userManager.GetRolesAsync(user);
            if (currentRoles.Count == 1 && string.Equals(currentRoles[0], desiredRole, StringComparison.Ordinal))
            {
                continue;
            }

            if (currentRoles.Count > 0)
            {
                var removeRolesResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
                EnsureSuccess(removeRolesResult, $"removing outdated roles from '{user.UserName}'");
            }

            var addRoleResult = await userManager.AddToRoleAsync(user, desiredRole);
            EnsureSuccess(addRoleResult, $"assigning role '{desiredRole}' to '{user.UserName}'");
        }
    }

    private static void EnsureSuccess(IdentityResult result, string operation)
    {
        if (result.Succeeded)
        {
            return;
        }

        var errorMessage = string.Join("; ", result.Errors.Select(error => error.Description));
        throw new InvalidOperationException($"Identity seeding failed while {operation}: {errorMessage}");
    }
}
