using MediatR;
using Organisation.Application.Common.DTO;


namespace Organisation.Application.EmployeeModule.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(string Id) : IRequest<EmployeeResponse>;