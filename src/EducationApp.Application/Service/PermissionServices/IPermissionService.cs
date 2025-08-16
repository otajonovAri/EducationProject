using EducationApp.Core.DTOs;
using EducationApp.Core.Entities;

namespace EducationApp.Application.Service.PermissionServices;

public interface IPermissionService
{
	Task CreateAsync(PermissionDto dto);
	List<Permission> GetAll();
}
