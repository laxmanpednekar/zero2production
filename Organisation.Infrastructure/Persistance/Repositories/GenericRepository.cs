using Dapper;
using Microsoft.IdentityModel.Tokens;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Application.Common.Utilities;
using Organisation.Domain.Common.Models;
using Organisation.Domain.Common.Utilities;
using Organisation.Infrastructure.Persistance.DataContext;
using System.Data;

namespace Organisation.Infrastructure.Persistance.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : IDbEntity
{
    protected readonly DapperDataContext _dapperDataContext;
    public GenericRepository(DapperDataContext dapperDataContext)
    {
        _dapperDataContext = dapperDataContext;
    }
    public async Task<IEnumerable<T>> GetAsync(QueryParameters queryParameters,params string[] selectData)
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add("pageNumber", queryParameters.PageNo, DbType.Int32, ParameterDirection.Input);
        parameters.Add("pageSize", queryParameters.PageSize, DbType.Int32, ParameterDirection.Input);

        if (!selectData.IsNullOrEmpty())
            parameters.Add("columns", typeof(T).GetDbTableColumnNames(selectData), DbType.String, ParameterDirection.Input);

        using (var connection = _dapperDataContext.Connection)
        {
            return await connection.QueryAsync<T>("spGetRecords", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<T> GetByIdAsync(string guid, params string[] selectData)
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add("id", guid, DbType.String, ParameterDirection.Input, size: 22);

        if (!selectData.IsNullOrEmpty())
            parameters.Add("columns", typeof(T).GetDbTableColumnNames(selectData), DbType.String, ParameterDirection.Input);

        using (var connection = _dapperDataContext.Connection)
        {
            return await connection.QuerySingleOrDefaultAsync<T>("spGetRecordsById", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<IEnumerable<T>> GetBySpecificColumnAsync(string columnName, string columnValue, params string[] selectData)
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add("columnName", columnName, DbType.String, ParameterDirection.Input, size: 60);
        parameters.Add("columnValue", columnValue, DbType.String, ParameterDirection.Input, size: 100);

        if (!selectData.IsNullOrEmpty())
            parameters.Add("columns", typeof(T).GetDbTableColumnNames(selectData), DbType.String, ParameterDirection.Input);

        using (var connection = _dapperDataContext.Connection)
        {
            return await connection.QueryAsync<T>("spGetRecordsBySpecificColumn", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<string> AddAsync(T entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add("columnNames", typeof(T).GetDbTableColumnNames(new string[0]), DbType.String, ParameterDirection.Input);
        parameters.Add("columnValues", typeof(T).GetColumnValuesForInsert(entity), DbType.String, ParameterDirection.Input);

        return await _dapperDataContext.Connection.ExecuteScalarAsync<string>("spInsertRecord", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
    }
    public async Task UpdateAsync(T entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add("columnsToUpdate", typeof(T).GetColumnValuesForUpdate(entity), DbType.String, ParameterDirection.Input);
        parameters.Add("id", entity.Id, DbType.String, ParameterDirection.Input, size: 22);
        await _dapperDataContext.Connection.ExecuteAsync("spUpdateRecord", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
    }
    public async Task SoftDeleteAsync(string id, bool softDeleteFromRelatedChildTables = false)
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add("id", id, DbType.String, ParameterDirection.Input, size: 22);
        await _dapperDataContext.Connection.ExecuteAsync("spSoftDeleteRecord", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);

        if (softDeleteFromRelatedChildTables)
            foreach (var associatedType in typeof(T).GetAssociatedTypes())
            {
                parameters = new DynamicParameters();
                parameters.Add("tableName", associatedType.Type.GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
                parameters.Add("foreignkeyColumnName", associatedType.ForeignKeyProperty.GetDbColumnName(), DbType.String, ParameterDirection.Input, size: 50);
                parameters.Add("foreignkeyColumnValue", id, DbType.String, ParameterDirection.Input, size: 22);

                await _dapperDataContext.Connection.ExecuteAsync("spSoftDeleteForeignKeyRecord", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
            }
    }
    public async Task<int> GetTotalCountAsync()
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);

        using (var connection = _dapperDataContext.Connection)
        {
            return await connection.QuerySingleOrDefaultAsync<int>("spGetTotalRecordsCount", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<bool> IsExistingAsync(string distinguishingUniqueKeyValue)
    {
        var parameters = new DynamicParameters();
        parameters.Add("tableName", typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add("distinguishingUniqueKeyColumnName", typeof(T).GetDistinguishingUniqueKeyName(), DbType.String, ParameterDirection.Input, size: 100);
        parameters.Add("distinguishingUniquekeyColumnValue", distinguishingUniqueKeyValue, DbType.String, ParameterDirection.Input, size: 100);

        using (var connection = _dapperDataContext.Connection)
        {
            return await connection.QuerySingleOrDefaultAsync<bool>("spDoesRecordExist", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
