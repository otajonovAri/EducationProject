using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.PermissionRepository;

public class PermissionRepo(EduDbContext context) : Repository<Permission>(context), IPermissionRepository
{
}