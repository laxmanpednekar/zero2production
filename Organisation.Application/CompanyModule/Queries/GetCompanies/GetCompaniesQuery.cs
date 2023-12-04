using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Company;

namespace Organisation.Application.CompanyModule.Queries.GetCompanies;

public record class GetCompaniesQuery(CompanyQueryParameters QueryParameters) : IRequest<PageList<CompanyResponse>>;
