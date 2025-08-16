using EducationApp.Application.Repositories.GroupRepository;
using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.AttendanceRepository;

public class AttendanceRepo(EduDbContext context) : Repository<Attendance>(context), IAttendanceRepository
{
}