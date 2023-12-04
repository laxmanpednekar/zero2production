using MediatR;

namespace Organisation.Application.CompanyModule.Commands.AddCompany;

public record class AddCompanyCommand(string Name, string Address, string Country) : IRequest<string>;

