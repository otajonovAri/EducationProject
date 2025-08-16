using EducationApp.Core;
using Microsoft.AspNetCore.Authorization;

namespace EducationApp.Application.Auth;

public class PermissionAuthorizeAttribute : AuthorizeAttribute
{
    public PermissionAuthorizeAttribute(Permission permission)
        : base(policy: permission.ToString())
    {
    }
}
