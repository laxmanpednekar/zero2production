using MediatR;
using Organisation.Application.Common.Exceptions;
using Organisation.Application.Common.Interfaces.Persistance;

namespace Organisation.Application.EmployeeModule.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var requiredEmplyee = await _unitOfWork.Employees.GetByIdAsync(request.Id);

        if (requiredEmplyee == null)
            throw new EmployeeNotFoundException($"The system does not have any employee with id={request.Id}");

        _unitOfWork.BeginTransaction();
        requiredEmplyee.Name = request.Name;
        requiredEmplyee.Age = request.Age;
        requiredEmplyee.Position = request.Position;
        requiredEmplyee.Salary = request.Salary;
        requiredEmplyee.ModifiedOn = request.ModifiedOn;
        await _unitOfWork.Employees.UpdateAsync(requiredEmplyee);
        _unitOfWork.CommitAndCloseConnection();
    }
}
