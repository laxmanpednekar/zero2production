
using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Employee;
using Organisation.Domain.Models;

namespace Organisation.Application.Common.Interfaces.Persistance;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    public Task<PageList<EmployeeResponse>> GetEmployeesByQueryAsync(EmployeeQueryParameters queryParameters);
}