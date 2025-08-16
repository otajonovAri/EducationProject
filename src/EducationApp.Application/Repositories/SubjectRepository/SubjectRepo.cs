using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.SubjectRepository;

public class SubjectRepo(EduDbContext context) : Repository<Subject>(context), ISubjectRepository
{
}