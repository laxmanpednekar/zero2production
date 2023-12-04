

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Organisation.Application.Common.Utilities;

namespace Organisation.Application.Common.ApplicationConfigOptions;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(GlobalConstants.ConfigurationSections.Jwt).Bind(options);
    }
}
