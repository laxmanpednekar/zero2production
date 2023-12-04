

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Organisation.Application.Common.Interfaces.Authentication;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Common.Errors;

namespace Organisation.Application.UserModule.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthTokenService _authTokenService;
    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IAuthTokenService authTokenService)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _authTokenService = authTokenService;
    }
    public async Task<ErrorOr<string>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies[GlobalConstants.RefreshTokenCookieKey];
        var user = await _unitOfWork.Users.GetUserByEmail(request.Email);

        if (user.RefreshToken is null || !user.RefreshToken.Equals(refreshToken))
            return Errors.User.InvalidRefreshToken();
        else if (user.RefreshTokenExpiryDate < DateTime.UtcNow)
            return Errors.User.RefreshTokenExpired();

        var accessToken = await _authTokenService.DoTokenCreationAsync(user);

        return accessToken;

    }
}
