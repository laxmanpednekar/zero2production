

using Microsoft.AspNetCore.Authorization;
using Organisation.Application.Common.Utilities;

namespace Organisation.Infrastructure.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context.User
                                 .Claims
                                 .Where(c => c.Type == GlobalConstants.CustomClaims.Permissions)
                                 .Select(c => c.Value);

        if (permissions.Contains(requirement.Permission))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
