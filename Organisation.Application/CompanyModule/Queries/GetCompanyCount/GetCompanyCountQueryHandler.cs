using MediatR;
using Organisation.Application.Common.Interfaces.Persistance;

namespace Organisation.Application.CompanyModule.Queries.GetCompanyCount;
public sealed class GetComapnyCountQueryHandler : IRequestHandler<GetCompanyCountQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetComapnyCountQueryHandler(IUnitOfWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }
    public async Task<int> Handle(GetCompanyCountQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Companies.GetTotalCountAsync();
    }
}
