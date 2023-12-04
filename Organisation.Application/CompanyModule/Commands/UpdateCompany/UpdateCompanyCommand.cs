using ErrorOr;
using MediatR;

namespace Organisation.Application.CompanyModule.Commands.UpdateCompany;
public record UpdateCompanyCommand(string Id, string Name, string Address, string Country) : IRequest<ErrorOr<Unit>>;