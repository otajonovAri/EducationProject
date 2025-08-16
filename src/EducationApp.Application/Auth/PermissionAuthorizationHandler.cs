using Microsoft.AspNetCore.Authorization;

namespace EducationApp.Application.Auth;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        var permissionClaims = context.User.FindAll("permissions").Select(c => c.Value);

        if (permissionClaims.Contains(requirement.RequiredPermission.ToString()) || permissionClaims.Contains("AdminPermission"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
