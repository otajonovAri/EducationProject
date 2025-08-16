using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.RolePermissionRepository;

public class RolePermissionRepo(EduDbContext context) : Repository<RolePermission>(context), IRolePermissionRepository
{
}