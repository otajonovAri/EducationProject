using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.RoleRepository;

public class RoleRepo(EduDbContext context) : Repository<Role>(context), IRoleRepository
{
}