using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Organisation.Infrastructure.Persistance.DataContext;

public sealed class DapperDataContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    public DapperDataContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SqlConnection");
    }
    public IDbConnection? Connection
    {
        get
        {
            if (_connection is null || _connection.State != ConnectionState.Open)
                _connection = new SqlConnection(_connectionString);
            return _connection;
        }
    }

    public IDbTransaction? Transaction
    {
        get
        {
            return _transaction;
        }

        set
        {
            _transaction = value;
        }
    }
}
