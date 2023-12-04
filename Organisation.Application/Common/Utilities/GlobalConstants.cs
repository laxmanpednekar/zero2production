

namespace Organisation.Application.Common.Utilities;

public static class GlobalConstants
{
    public const string Errors = "errors";
    public const string ApplicationName = "Organisation.API";
    public const string RefreshTokenCookieKey = "refreshToken";

    public static class ConfigurationSections
    {
        public const string Jwt = "Jwt";
        public const string My3rdpartyProductOptions = "My3rdpartyProductoptions";
    }

    public static class CustomClaims
    {
        public const string Permissions = "permissions";
    }
}
