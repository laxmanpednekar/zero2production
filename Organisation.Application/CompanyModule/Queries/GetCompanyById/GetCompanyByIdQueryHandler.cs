using ErrorOr;
using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Exceptions;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Domain.Common.Errors;

namespace Organisation.Application.CompanyModule.Queries.GetCompanyById;
public sealed class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, ErrorOr<CompanyResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<CompanyResponse>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var requiredCompany = await _unitOfWork.Companies.GetByIdAsync(request.Id);

        if (requiredCompany == null)
            return Errors.Company.CompanyDoestNotExist($"The system does not have any company with id={request.Id}");//throw new CompanyNotFoundException($"The system does not have any company with id={request.Id}");//return NotFound(requiredCompany);

        return new CompanyResponse(requiredCompany.Name, requiredCompany.Address, requiredCompany.Country);
    }
}