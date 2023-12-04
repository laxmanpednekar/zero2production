

using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Company;
using Organisation.Domain.Models;

namespace Organisation.Application.Common.Interfaces.Persistance;

public interface ICompanyRepository : IGenericRepository<Company>
{
    public Task<PageList<CompanyResponse>> GetCompaniesByQueryAsync(CompanyQueryParameters queryParameters);
}

