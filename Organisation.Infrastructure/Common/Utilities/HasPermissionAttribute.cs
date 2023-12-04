

using Microsoft.AspNetCore.Authorization;
using Organisation.Domain.Common.Enums;

namespace Organisation.Infrastructure.Common.Utilities;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
	public HasPermissionAttribute(Permission permission) : base(policy: Convert.ToString(permission))
	{

	}
}
