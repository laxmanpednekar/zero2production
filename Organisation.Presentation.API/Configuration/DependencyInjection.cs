using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Organisation.Application.Common.ApplicationConfigOptions;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.ThirdPartyServices;
using Organisation.Infrastructure.Persistance;
using Organisation.Presentation.API.Common.Exceptions;
using Organisation.Presentation.API.Common.Mappings;
using Organisation.Presentation.API.Swagger;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Organisation.Presentation.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            options.ExampleFilters();
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            //options.ApiVersionReader = new QueryStringApiVersionReader("organisationapp-api-version"); //query string custom versioning
            //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version"); //header versioning
        });
        //configure swagger to support api versioning
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerExamplesFromAssemblyOf<Program>();

        services.ConfigureOptions<My3rdPartyProductOptionsSetup>();
        services.AddHttpClient<ExternalProductsService>((serviceProvider, httpClient) => {
            var my3rdPartyProductOptions = serviceProvider.GetRequiredService<IOptions<My3rdPartyProductOptions>>().Value;
            httpClient.BaseAddress = new Uri(my3rdPartyProductOptions.BaseURI);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", my3rdPartyProductOptions.Token);
        }).ConfigurePrimaryHttpMessageHandler(() => {
            return new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            };
        }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        services.AddMappings();
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
