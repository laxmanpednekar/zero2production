
using Organisation.Domain.Common.Models;
using Organisation.Domain.Common.Utilities;

namespace Organisation.Application.Common.Interfaces.Persistance;

public interface IGenericRepository<T>
    where T : IDbEntity
{
    Task<IEnumerable<T>> GetAsync(QueryParameters queryParameters,params string[] selectData);
    Task<T> GetByIdAsync(string guid, params string[] selectData);
    Task<IEnumerable<T>> GetBySpecificColumnAsync(string columnName, string columnValue, params string[] selectData);
    Task<string> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task SoftDeleteAsync(string id, bool softDeleteFromRelatedChildTables = false);
    Task<int> GetTotalCountAsync();
    Task<bool> IsExistingAsync(string distinguishingUniqueKeyValue);
}
