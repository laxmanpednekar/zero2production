using ErrorOr;
using MediatR;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Domain.Models;

namespace Organisation.Application.EmployeeModule.Commands.AddEmployee;

public sealed class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, ErrorOr<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<string>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.BeginTransaction();
        var employeeId = await _unitOfWork.Employees.AddAsync(new Employee
        {
            Name = request.CreateEmployeeRequest.Name,
            Age = request.CreateEmployeeRequest.Age,
            Position = request.CreateEmployeeRequest.Position,
            CreatedOn = request.CreateEmployeeRequest.CreatedOn,
            ModifiedOn = request.CreateEmployeeRequest.CreatedOn,
            CompanyId = request.CreateEmployeeRequest.CompanyId,
            Salary = request.CreateEmployeeRequest.Salary

        });
        _unitOfWork.CommitAndCloseConnection();

        return employeeId;
    }
}
