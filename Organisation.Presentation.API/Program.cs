using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Organisation.Application.Common.Utilities;
using Organisation.Application.Configuration;
using Organisation.Infrastructure.Configuration;
using Organisation.Presentation.API.Configuration;
using Organisation.Presentation.API.Configuration.AzureKeyVault;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

try
{
    Log.Information("{ApplicationName} is starting up.", GlobalConstants.ApplicationName);

    var builder = WebApplication.CreateBuilder(args);

    // register services of all the layers

    builder.Services
            .AddApplication()
            .AddPresentation()
            .AddInfrastructure();

    if(builder.Environment.IsProduction())
        builder.ConfigureAzureKeyVault();


    var app = builder.Build();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {

            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                c.RoutePrefix = "api/documentation";
                c.DefaultModelExpandDepth(2);
                c.DocExpansion(DocExpansion.None);
                c.DisplayRequestDuration();
            }
        });
    }

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "{ApplicationName} failed to start up.", GlobalConstants.ApplicationName);
}
finally {
   Log.CloseAndFlush();
}


