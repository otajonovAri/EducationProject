using EducationApp.Core.DTOs;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.RoleServices;

public class RoleService : IRoleService
{
	private readonly EduDbContext _context;

	public RoleService(EduDbContext context)
	{
		_context = context;
	}

	public async Task CreateAsync(RoleDto dto)
	{
		if (await _context.Roles.AnyAsync(r => r.Name == dto.Name))
			throw new Exception("Role already exists");

		var role = new Role { Name = dto.Name };
		_context.Roles.Add(role);
		await _context.SaveChangesAsync();
	}

	public async Task AssignPermissionAsync(RolePermissionDto dto)
	{
		if (await _context.RolePermissions.AnyAsync(rp => rp.RoleId == dto.RoleId && rp.PermissionId == dto.PermissionId))
			throw new Exception("Permission already assigned to this role");

		var rolePermission = new RolePermission
		{
			RoleId = dto.RoleId,
			PermissionId = dto.PermissionId
		};

		_context.RolePermissions.Add(rolePermission);
		await _context.SaveChangesAsync();
	}
}
