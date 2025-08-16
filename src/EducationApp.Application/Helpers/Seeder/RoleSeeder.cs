using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EducationApp.Application.Helpers.Seeder;

public class RoleSeeder(EduDbContext context)
{
    public async Task SeedAsync()
    {
        var roles = new[] { "Admin", "User" }
            .Select(name => new Role
            {
                Name = name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            }).ToList();

        var existing = await context.Roles.Select(r => r.Name).ToListAsync();

        var newRoles = roles.Where(r => !existing.Contains(r.Name)).ToList();

        if (newRoles.Any())
        {
            context.Roles.AddRange(newRoles);
            await context.SaveChangesAsync();
            Log.Information($"Added {newRoles.Count} new roles.");
        }
        else
        {
            Log.Information("faRoles already seeded.");
        }
    }
}