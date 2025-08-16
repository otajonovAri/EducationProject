using EducationApp.Core.DTOs;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.PermissionServices;

public class PermissionService : IPermissionService
{
	private readonly EduDbContext _context;

	public PermissionService(EduDbContext context)
	{
		_context = context;
	}

	public async Task CreateAsync(PermissionDto dto)
	{
		if (await _context.Permissions.AnyAsync(p => p.Name == dto.Name))
			throw new Exception("Permission already exists");

		var permission = new Permission
		{
			Name = dto.Name,
			Description = dto.Description
		};

		_context.Permissions.Add(permission);
		await _context.SaveChangesAsync();
	}

	public List<Permission> GetAll()
	{
		var permissions = _context.Permissions.ToList();

		return permissions;
    }
}
