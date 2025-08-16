using EducationApp.Application.Helpers.PasswordHasher;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EducationApp.Application.Helpers.Seeder;

public class AdminUserSeeder(EduDbContext context , IPasswordHasher passwordHasher)
{
    public async Task SeedAsync()
    {
        var adminEmail = "admin@example.com";
        if (!await context.Users.AnyAsync(u => u.Email == adminEmail))
        {
            var salt = passwordHasher.GenerateSalt();
            var password = "adminadmin";
            var passwordHash = passwordHasher.Encrypt(password, salt);

            var user = new User
            {
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "0000000000",
                Password = password,
                PasswordHash = passwordHash,
                PasswordSolt = salt,
                DateBirth = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Gender = EducationApp.Core.Enums.Gender.Male
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            Log.Information("Admin user created.");
        }
        else
        {
            Log.Information("Admin user already exists.");
        }
    }
}