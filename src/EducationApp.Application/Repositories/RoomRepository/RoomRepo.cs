using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.RoomRepository;

public class RoomRepo(EduDbContext context) : Repository<Room>(context), IRoomRepository
{
}