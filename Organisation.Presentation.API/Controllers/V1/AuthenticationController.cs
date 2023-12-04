using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organisation.Application.Common.DTO;
using Organisation.Application.UserModule.Commands.RefreshToken;
using Organisation.Application.UserModule.Commands.RegisterUser;
using Organisation.Application.UserModule.Queries.LoginUser;

namespace Organisation.Presentation.API.Controllers.V1;

[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[AllowAnonymous]
public sealed class AuthenticationController : BaseAPIController
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    public AuthenticationController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var registerUserCommand = _mapper.Map<RegisterUserCommand>(request);
        var result = await _sender.Send(registerUserCommand);

        return result.Match(
                r => NoContent(),
                errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var loginUserQuery = _mapper.Map<LoginUserQuery>(request);
        var result = await _sender.Send(loginUserQuery);

        return result.Match(
                r => Ok(r),
                errors => Problem(errors)
        );
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
    {
        var refreshTokenCommand = _mapper.Map<RefreshTokenCommand>(request);
        var result = await _sender.Send(refreshTokenCommand);

        return result.Match(
                r => Ok(r),
                errors => Problem(errors)
        );
    }

}
