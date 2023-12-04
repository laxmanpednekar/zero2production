
using ErrorOr;
using MediatR;
using Organisation.Application.Common.DTO;

namespace Organisation.Application.CompanyModule.Queries.GetCompanyById;

public record GetCompanyByIdQuery(string Id) : IRequest<ErrorOr<CompanyResponse>>;
