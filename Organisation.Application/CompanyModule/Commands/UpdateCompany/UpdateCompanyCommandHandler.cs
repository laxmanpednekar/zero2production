using ErrorOr;
using MediatR;
using Organisation.Application.Common.Exceptions;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Domain.Common.Errors;

namespace Organisation.Application.CompanyModule.Commands.UpdateCompany;

public sealed class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand,ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var requiredCompany = await _unitOfWork.Companies.GetByIdAsync(request.Id);

        if (requiredCompany == null)
            return Errors.Company.CompanyDoestNotExist($"The system does not have any company with id={request.Id}");

        _unitOfWork.BeginTransaction();
        requiredCompany.Name = request.Name ?? requiredCompany.Name;
        requiredCompany.Address = request.Address ?? requiredCompany.Address;
        requiredCompany.Country = request.Country ?? requiredCompany.Country;

        await _unitOfWork.Companies.UpdateAsync(requiredCompany);

        _unitOfWork.CommitAndCloseConnection();

        return Unit.Value;
    }
}