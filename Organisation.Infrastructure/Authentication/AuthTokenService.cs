

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Organisation.Application.Common.ApplicationConfigOptions;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Authentication;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.User.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Organisation.Infrastructure.Authentication;

public sealed class AuthTokenService : IAuthTokenService
{
    private readonly JwtOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    public AuthTokenService(IOptions<JwtOptions> options,IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _options = options.Value;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork= unitOfWork;
    }

    public async Task<string> DoTokenCreationAsync(User user)
    {
        var accessToken = await GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken();
        SetRefreshTokenAsHttpOnlyCookie(newRefreshToken);

        _unitOfWork.BeginTransaction();
        user.RefreshToken = newRefreshToken.TokenValue;
        user.RefreshTokenExpiryDate = newRefreshToken.Expires;
        await _unitOfWork.Users.UpdateAsync(user);
        _unitOfWork.CommitAndCloseConnection();

        return accessToken;
    }

    private RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            TokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(7)
        };
    }

    private async Task<string> GenerateAccessToken(User user)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id),
           new Claim(JwtRegisteredClaimNames.Email,user.Email)
        };

        foreach (var permission in await _unitOfWork.Users.GetUserPermissions(user))
            claims.Add(new Claim(GlobalConstants.CustomClaims.Permissions, permission.Name));

         var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha512Signature
        );

        var token = new JwtSecurityToken(
          _options.Issuer,
          _options.Audience,
          claims,
          null,
          DateTime.UtcNow.AddHours(1),
          signingCredentials
      );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private void SetRefreshTokenAsHttpOnlyCookie(RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires,
        };

        var httpContext = _httpContextAccessor.HttpContext;
        httpContext?.Response.Cookies.Append(GlobalConstants.RefreshTokenCookieKey, refreshToken.TokenValue, cookieOptions);
    }
}
