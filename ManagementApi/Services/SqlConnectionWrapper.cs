namespace ManagementApi.Services
{
    using Dapper;
    using ManagementApi.Services.Interfaces;
    using System.Data.SqlClient;

    public class SqlConnectionWrapper : ISqlConnectionWrapper
    {
        private readonly SqlConnection _sqlConnection;

        public SqlConnectionWrapper(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object parameters)
        {
            return await _sqlConnection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }
    }
}
