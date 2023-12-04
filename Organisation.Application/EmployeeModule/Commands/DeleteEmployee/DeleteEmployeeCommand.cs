using MediatR;

namespace Organisation.Application.EmployeeModule.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(string Id, bool DeleteAssociations) : IRequest;
