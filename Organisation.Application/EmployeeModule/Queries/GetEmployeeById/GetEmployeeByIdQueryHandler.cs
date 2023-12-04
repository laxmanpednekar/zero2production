using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Exceptions;
using Organisation.Application.Common.Interfaces.Persistance;

namespace Organisation.Application.EmployeeModule.Queries.GetEmployeeById;

public sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<EmployeeResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var requiredEmployee = await _unitOfWork.Employees.GetByIdAsync(request.Id);

        if (requiredEmployee == null)
            throw new EmployeeNotFoundException($"The system does not have any employee with id={request.Id}");

        return new EmployeeResponse(
            requiredEmployee.Name,
            requiredEmployee.Age,
            requiredEmployee.Position,
            requiredEmployee.Salary,
            requiredEmployee.CreatedOn);
    }

}
