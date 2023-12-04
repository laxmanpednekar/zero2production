using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Domain.User.Models;

namespace Organisation.Application.UserModule.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Unit>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Command.Password);

        _unitOfWork.BeginTransaction();
        var userId = await _unitOfWork.Users.AddAsync(new User
        {
            Email = request.Command.Email,
            UserName = request.Command.UserName,
            PasswordHash = passwordHash
        });
        _unitOfWork.CommitAndCloseConnection();

        return Unit.Value;
    }
}
