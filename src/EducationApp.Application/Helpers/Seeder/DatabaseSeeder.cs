using EducationApp.Application.Helpers.PasswordHasher;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Helpers.Seeder;

public class DatabaseSeeder(IPasswordHasher passwordHasher, EduDbContext context)
{
    public void SeedData()
    {
        if (!context.Permissions.Any())
        {
            context.Permissions.Add(new Permission
            {
                Id = 1,
                Name = "AdminPermission",
                Description = "AdminPermission",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }

        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Id = 1, Name = "Admin", IsDeleted = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Role { Id = 2, Name = "User", IsDeleted = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            context.SaveChanges();
        }

        if (!context.RolePermissions.Any())
        {
            context.RolePermissions.Add(new RolePermission
            {
                Id = 1,
                RoleId = 1,
                PermissionId = 1,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var salt = "cf5392c2-0d03-4cc7-9eb5-4a4016b4a712";
            var password = "adminadmin";
            var passwordHash = passwordHasher.Encrypt(password, salt); // You must implement this

            context.Users.Add(new User
            {
                Id = 1,
                DateBirth = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Email = "admin@example.com",
                FirstName = "admin",
                LastName = "admin",
                Gender = Core.Enums.Gender.Male,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Password = password,
                PasswordHash = passwordHash,
                PasswordSolt = salt,
                PhoneNumber = "admin",
                RefreshToken = "7Q+nHoS2n9yoKZwBQFq1k3eJZ4ppZjXsOiB7VoO9TisAuRY6fOHRAy2RLixz9Esnrz7HTSfqkWY+7KFkEKnTiw==",
            });

            context.SaveChanges();
        }

        if (!context.UserRoles.Any())
        {
            context.UserRoles.Add(new UserRole
            {
                Id = 1,
                UserId = 1,
                RoleId = 1,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }
}
