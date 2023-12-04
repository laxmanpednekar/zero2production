using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Organisation.Application.Common.Utilities;

namespace Organisation.Application.Common.ApplicationConfigOptions;

public sealed class My3rdPartyProductOptionsSetup : IConfigureOptions<My3rdPartyProductOptions>
{
    private readonly IConfiguration _configuration;
    public My3rdPartyProductOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(My3rdPartyProductOptions options)
    {
        _configuration.GetSection(GlobalConstants.ConfigurationSections.My3rdpartyProductOptions).Bind(options);
    }
}
