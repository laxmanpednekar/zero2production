

using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Common.Utilities;

namespace Organisation.Application.CompanyModule.Queries.GetCompanies;

public sealed class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, PageList<CompanyResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetCompaniesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PageList<CompanyResponse>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Companies.GetCompaniesByQueryAsync(request.QueryParameters);
    }
}
