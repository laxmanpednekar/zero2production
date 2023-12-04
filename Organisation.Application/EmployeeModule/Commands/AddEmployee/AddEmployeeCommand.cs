

using ErrorOr;
using MediatR;
using Organisation.Application.Common.DTO;

namespace Organisation.Application.EmployeeModule.Commands.AddEmployee;
public record AddEmployeeCommand(CreateEmployeeRequest CreateEmployeeRequest) : IRequest<ErrorOr<string>>;
