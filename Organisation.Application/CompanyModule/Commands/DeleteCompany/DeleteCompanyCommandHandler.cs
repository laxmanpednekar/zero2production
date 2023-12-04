using MediatR;
using Organisation.Application.Common.Exceptions;
using Organisation.Application.Common.Interfaces.Persistance;

namespace Organisation.Application.CompanyModule.Commands.DeleteCompany;

public sealed class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyToDelete = await _unitOfWork.Companies.GetByIdAsync(request.Id);

        if (companyToDelete == null)
            throw new CompanyNotFoundException($"The system does not have any company with id={request.Id}");

        _unitOfWork.BeginTransaction();
        await _unitOfWork.Companies.SoftDeleteAsync(request.Id, request.DeleteAssociations);
        _unitOfWork.CommitAndCloseConnection();
    }
}
