using EducationApp.Application.Repositories.Repository;
using File = EducationApp.Core.Entities.File;

namespace EducationApp.Application.Repositories.FileRepository;

public interface IFileRepository : IRepository<File>
{
}
