

using FluentValidation;
using Organisation.Application.Common.Utilities;

namespace Organisation.Application.UserModule.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(rc => rc.Command.Email).NotNull().NotEmpty().WithErrorCode("VALERRUSR001").WithMessage("User email is mandatory.");
        RuleFor(rc => rc.Command.Email).Custom((email, context) => {
            if (!email.IsValidEmail())
                context.AddFailure("VALERRUSR002", "Invalid user email.");
        });
        RuleFor(rc => rc.Command.Password).NotNull().NotEmpty().WithErrorCode("VALERRUSR003").WithMessage("User password is mandatory.");
        RuleFor(rc => rc.Command.Password).Custom((password, context) => {
            if (!password.IsValidPassword())
                context.AddFailure("VALERRUSR004", "Invalid user password.");
        });
    }
}
