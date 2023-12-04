using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Employee;

namespace Organisation.Application.EmployeeModule.Queries.GetEmployees;
public record GetEmployeesQuery(EmployeeQueryParameters QueryParameters) : IRequest<PageList<EmployeeResponse>>;