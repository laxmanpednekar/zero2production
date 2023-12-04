
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Organisation.Application.Common.ApplicationConfigOptions;
using Organisation.Application.Common.Interfaces.Authentication;
using Organisation.Infrastructure.Authentication;
using Organisation.Infrastructure.Authorization;
using Organisation.Infrastructure.Persistance.DataContext;

namespace Organisation.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<DapperDataContext>();
        services.AddScoped<IAuthTokenService, AuthTokenService>();
        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        return services;
    }
}
