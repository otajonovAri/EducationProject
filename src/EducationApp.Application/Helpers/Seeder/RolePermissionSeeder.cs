using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EducationApp.Application.Helpers.Seeder;

public class RolePermissionSeeder(EduDbContext context)
{
    public async Task SeedAsync()
    {
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        var permissions = await context.Permissions.ToListAsync();

        var existing = await context.RolePermissions
            .Where(rp => rp.RoleId == adminRole.Id)
            .Select(rp => rp.PermissionId)
            .ToListAsync();

        var newRolePermissions = permissions
            .Where(p => !existing.Contains(p.Id))
            .Select(p => new RolePermission
            {
                RoleId = adminRole.Id,
                PermissionId = p.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            }).ToList();

        if (newRolePermissions.Any())
        {
            context.RolePermissions.AddRange(newRolePermissions);
            await context.SaveChangesAsync();
            Log.Information($"Added {newRolePermissions.Count} permissions to Admin role.");
        }
        else
        {
            Log.Information("Admin role already has permissions.");
        }
    }
}