using Dapper;
using Organisation.Application.Common.Interfaces.Persistance;
using Organisation.Domain.Permission.Models;
using Organisation.Domain.User.Models;
using Organisation.Infrastructure.Persistance.DataContext;
using System.Data;

namespace Organisation.Infrastructure.Persistance.Repositories;

public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return (await GetBySpecificColumnAsync("Email", email)).AsQueryable().FirstOrDefault();
    }

    public async Task<IEnumerable<Permission>> GetUserPermissions(User user)
    {
        var parameters = new DynamicParameters();
        parameters.Add("userId", user.Id, DbType.String, ParameterDirection.Input, size: 22);

        using (var connection = _dapperDataContext.Connection)
        {
            return await connection.QueryAsync<Permission>("spGetUserPermissions", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
