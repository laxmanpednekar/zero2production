using MediatR;
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Exceptions;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Domain.Models;

namespace Organisation.Application.CompanyModule.Commands.AddCompany;

public sealed class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<string> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Companies.IsExistingAsync(request.Name))
            throw new DuplicateCompanyException($"Company with name {request.Name} already exits");//Conflict(companyRequest);

        _unitOfWork.BeginTransaction();
        var companyId = await _unitOfWork.Companies.AddAsync(new Company
        {
            Name = request.Name,
            Address = request.Address,
            Country = request.Country,
        });
        _unitOfWork.CommitAndCloseConnection();

        return companyId;
    }
}
