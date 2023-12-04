using ErrorOr;
using MediatR;
using Organisation.Application.Common.DTO;

namespace Organisation.Application.UserModule.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserRequest Command) : IRequest<ErrorOr<Unit>>;
