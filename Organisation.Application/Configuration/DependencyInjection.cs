using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Organisation.Application.Common.ApplicationConfigOptions;
using Organisation.Application.Common.PipelineBehaviours;
using Serilog;

namespace Organisation.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddSerilog();
        services.AddHttpContextAccessor();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
