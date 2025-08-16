using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.GroupSubjectRepository;

public class GroupSubjectRepo(EduDbContext context) : Repository<GroupSubject>(context), IGroupSubjectRepository
{
}