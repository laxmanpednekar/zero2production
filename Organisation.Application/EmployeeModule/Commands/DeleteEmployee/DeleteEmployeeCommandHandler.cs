using MediatR;
using Organisation.Application.Common.Exceptions;
using Organisation.Application.Common.Interfaces.Persistance;

namespace Organisation.Application.EmployeeModule.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeToDelete = await _unitOfWork.Employees.GetByIdAsync(request.Id);

        if (employeeToDelete == null)
            throw new EmployeeNotFoundException($"The system does not have any employee with id={request.Id}");

        _unitOfWork.BeginTransaction();
        await _unitOfWork.Employees.SoftDeleteAsync(request.Id, request.DeleteAssociations);
        _unitOfWork.CommitAndCloseConnection();
    }
}
