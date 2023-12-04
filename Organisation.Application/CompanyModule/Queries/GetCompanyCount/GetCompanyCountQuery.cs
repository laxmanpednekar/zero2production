using MediatR;

namespace Organisation.Application.CompanyModule.Queries.GetCompanyCount;
public record class GetCompanyCountQuery() : IRequest<int>;