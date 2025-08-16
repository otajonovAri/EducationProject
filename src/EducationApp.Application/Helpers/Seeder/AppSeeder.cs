using Serilog;

namespace EducationApp.Application.Helpers.Seeder;

public class AppSeeder(
    PermissionSeeder permissionSeeder,
    RoleSeeder roleSeeder,
    AdminUserSeeder adminUserSeeder,
    RolePermissionSeeder rolePermissionSeeder,
    UserRoleSeeder userRoleSeeder)
{
    public async Task SeedAsync()
    {
        Log.Information("Starting database seeding...");
        await permissionSeeder.SeedAsync();
        await roleSeeder.SeedAsync();
        await adminUserSeeder.SeedAsync();
        await rolePermissionSeeder.SeedAsync();
        await userRoleSeeder.SeedAsync();
        Log.Information("✅ Database seeding finished.");
    }
}