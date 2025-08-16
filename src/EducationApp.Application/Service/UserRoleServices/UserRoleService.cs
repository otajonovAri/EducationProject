using EducationApp.Core.DTOs;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.UserRoleServices;

public class UserRoleService(EduDbContext context)
{
    public async Task AssignAsync(UserRoleDto dto)
	{
		if (await context.UserRoles.AnyAsync(ur => ur.UserId == dto.UserId && ur.RoleId == dto.RoleId))
			throw new Exception("User already has this role");

		var userRole = new UserRole
		{
			UserId = dto.UserId,
			RoleId = dto.RoleId
		};

		context.UserRoles.Add(userRole);
		await context.SaveChangesAsync();
	}
}
