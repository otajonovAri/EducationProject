using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.StaffRepository;

public class StaffRepo(EduDbContext context) : Repository<Staff>(context), IStaffRepository
{
}