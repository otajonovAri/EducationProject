using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EducationApp.Application.Helpers.Seeder;

public class UserRoleSeeder(EduDbContext context)
{
    public async Task SeedAsync()
    {
        var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "admin@example.com");
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

        var exists = await context.UserRoles.AnyAsync(ur => ur.UserId == adminUser.Id && ur.RoleId == adminRole.Id);

        if (!exists)
        {
            var userRole = new UserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync();
            Log.Information("Admin user assigned to Admin role.");
        }
        else
        {
            Log.Information("Admin user already has Admin role.");
        }
    }
}