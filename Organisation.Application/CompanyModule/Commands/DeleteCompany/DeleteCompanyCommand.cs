using MediatR;

namespace Organisation.Application.CompanyModule.Commands.DeleteCompany;

public record DeleteCompanyCommand(string Id, bool DeleteAssociations) : IRequest;