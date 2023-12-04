

using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Employee;
using Organisation.Domain.Models;
using Organisation.Infrastructure.Persistance.DataContext;

namespace Organisation.Infrastructure.Persistance.Repositories;

public sealed class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async Task<PageList<EmployeeResponse>> GetEmployeesByQueryAsync(EmployeeQueryParameters queryParameters)
    {
        var employees = (await GetAsync(queryParameters, "Name", "Age", "Position", "Salary", "CreatedOn"))
                        .AsQueryable()
                        .Select(e => new EmployeeResponse(e.Name,e.Age,e.Position,e.Salary,e.CreatedOn));

        if (!string.IsNullOrEmpty(queryParameters.Name))
            employees = employees.Where(e => e.Name.ToLowerInvariant().Contains(queryParameters.Name.ToLowerInvariant()));

        if (!string.IsNullOrEmpty(queryParameters.SortBy))
            if (typeof(Employee).GetProperty(queryParameters.SortBy) != null)
                employees = employees.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);


        var pagedEmployees = PageList<EmployeeResponse>.Create(employees, queryParameters.PageNo, queryParameters.PageSize, 2000000);

        return pagedEmployees;
    }
}