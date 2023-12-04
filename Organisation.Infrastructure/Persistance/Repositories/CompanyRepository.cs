

using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Company;
using Organisation.Domain.Models;
using Organisation.Infrastructure.Persistance.DataContext;

namespace Organisation.Infrastructure.Persistance.Repositories;

public sealed class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async  Task<PageList<CompanyResponse>> GetCompaniesByQueryAsync(CompanyQueryParameters queryParameters)
    {
        var companies = (await GetAsync(queryParameters, "Name", "Address", "Country")).AsQueryable().Select(c => new CompanyResponse(c.Name,c.Address,c.Country));

        var pagedCompanies = PageList<CompanyResponse>.Create(companies, queryParameters.PageNo, queryParameters.PageSize, await GetTotalCountAsync());

        return pagedCompanies;
    }
}
