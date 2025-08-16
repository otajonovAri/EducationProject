using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.StudentRepository;

public class StudentRepo(EduDbContext context) : Repository<Student>(context), IStudentRepository
{
}