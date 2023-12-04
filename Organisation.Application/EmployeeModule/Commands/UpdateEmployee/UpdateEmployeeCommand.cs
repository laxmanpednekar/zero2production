using MediatR;

namespace Organisation.Application.EmployeeModule.Commands.UpdateEmployee;

public record UpdateEmployeeCommand(string Id, string Name, int Age, string Position, DateTime ModifiedOn, decimal Salary, string CompanyId) : IRequest;
