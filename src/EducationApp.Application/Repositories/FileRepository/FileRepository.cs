using EducationApp.Application.Repositories.Repository;
using EducationApp.DataAccess.Database;
using File = EducationApp.Core.Entities.File;

namespace EducationApp.Application.Repositories.FileRepository;

public class FileRepository(EduDbContext context) : Repository<File>(context), IFileRepository
{
}
