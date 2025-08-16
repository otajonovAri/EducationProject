using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Repositories.UserRepository;

public class UserRepository(EduDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User> EmailFindAsync(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);

        return user;
    }
}