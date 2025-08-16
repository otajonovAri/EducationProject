using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;

namespace EducationApp.Application.Repositories.UserRepository;

public interface IUserRepository : IRepository<User>
{
    Task<User> EmailFindAsync(string email);
}