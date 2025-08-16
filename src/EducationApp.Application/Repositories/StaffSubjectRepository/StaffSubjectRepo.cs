using EducationApp.Application.Repositories.Repository;
using EducationApp.Application.Repositories.StaffRepository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.StaffSubjectRepository;

public class StaffSubjectRepo(EduDbContext context) : Repository<StaffSubject>(context), IStaffSubjectRepository
{
}