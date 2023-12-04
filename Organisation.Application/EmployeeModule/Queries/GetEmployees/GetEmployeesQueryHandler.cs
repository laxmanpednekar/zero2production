using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;

namespace Organisation.Application.EmployeeModule.Queries.GetEmployees;

public sealed class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PageList<EmployeeResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetEmployeesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<PageList<EmployeeResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Employees.GetEmployeesByQueryAsync(request.QueryParameters);
    }
}
