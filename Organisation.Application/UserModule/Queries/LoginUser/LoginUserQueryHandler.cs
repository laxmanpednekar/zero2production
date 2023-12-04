using ErrorOr;
using MapsterMapper;
using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Authentication;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Domain.Common.Errors;

namespace Organisation.Application.UserModule.Queries.LoginUser;

public sealed class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ErrorOr<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthTokenService _authTokenService;
    public LoginUserQueryHandler(IUnitOfWork unitOfWork,IAuthTokenService authTokenService)
    {
        _unitOfWork = unitOfWork;
        _authTokenService = authTokenService;
    }
    public async Task<ErrorOr<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetUserByEmail(request.Email);

        if (user is not null && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) {
            var accessToken = await _authTokenService.DoTokenCreationAsync(user);
            return accessToken;
        }
            
        else
            return Errors.User.IncorrectEmailOrPassword();
  
    }
}
