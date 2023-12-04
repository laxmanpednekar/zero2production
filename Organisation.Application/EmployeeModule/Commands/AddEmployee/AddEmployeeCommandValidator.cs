
using FluentValidation;

namespace Organisation.Application.EmployeeModule.Commands.AddEmployee;

public sealed class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
{
	public AddEmployeeCommandValidator()
	{
		RuleFor(ec=>ec.CreateEmployeeRequest.Name).NotNull().NotEmpty().WithErrorCode("VALERREMP001").WithMessage("Employee Name is mandatory.");
        RuleFor(ec => ec.CreateEmployeeRequest.Age).GreaterThanOrEqualTo(18).WithErrorCode("VALERREMP002").WithMessage("Employee Age must be between 18-150");
        RuleFor(ec => ec.CreateEmployeeRequest.Age).LessThanOrEqualTo(150).WithErrorCode("VALERREMP002").WithMessage("Employee Age must be between 18-150");
    }
}
