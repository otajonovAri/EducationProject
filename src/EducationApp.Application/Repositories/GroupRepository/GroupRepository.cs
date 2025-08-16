using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.GroupRepository;

public class GroupRepository(EduDbContext context) : Repository<Group>(context), IGroupRepository
{
}