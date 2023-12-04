using ErrorOr;
using MediatR;

namespace Organisation.Application.UserModule.Queries.LoginUser;
public record LoginUserQuery(string Email, string Password) : IRequest<ErrorOr<string>>;