
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EducationApp.Application.Helpers.Seeder;

public class PermissionSeeder(EduDbContext context)
{
    public async Task SeedAsync()
    {
        var existingNames = await context.Permissions
            .Select(p => p.Name)
            .ToListAsync();

        var enumValues = Enum.GetValues(typeof(EducationApp.Core.Permission))
            .Cast<EducationApp.Core.Permission>()
            .Select(p => new Permission
            {
                Name = p.ToString(),
                Description = p.ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            }).ToList();

        var newPermissions = enumValues
            .Where(p => !existingNames.Contains(p.Name))
            .ToList();

        if (newPermissions.Any())
        {
            context.Permissions.AddRange(newPermissions);
            await context.SaveChangesAsync();
            Log.Information($"{newPermissions.Count} new permissions.");
        }
        else
        {
            Log.Information("Permissions already seeded.");
        }
    }
}