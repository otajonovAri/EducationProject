using EducationApp.Core;
using Microsoft.AspNetCore.Authorization;

namespace EducationApp.Application.Auth;

public class PermissionRequirement : IAuthorizationRequirement
{
    public Permission RequiredPermission { get; }

    public PermissionRequirement(Permission permission)
    {
        RequiredPermission = permission;
    }
}
