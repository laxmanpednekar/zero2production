

using ErrorOr;
using MediatR;

namespace Organisation.Application.UserModule.Commands.RefreshToken;

public record RefreshTokenCommand(string Email) : IRequest<ErrorOr<string>>;