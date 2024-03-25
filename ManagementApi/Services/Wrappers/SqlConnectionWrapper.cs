namespace ManagementApi.Services.Wrappers
{
    using Dapper;
    using Interfaces;
    using System.Data.SqlClient;

    public class SqlConnectionWrapper : ISqlConnectionWrapper
    {
        private readonly SqlConnection _sqlConnection;

        public SqlConnectionWrapper(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Releases all resources used by <see cref='System.ComponentModel.Component'/>.
        /// </summary>
        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns><see cref='T?'/></returns>
        public async Task<T?> QuerySingleOrDefaultAsync<T>(string sQL, object parameters)
        {
            return await _sqlConnection.QuerySingleOrDefaultAsync<T>(sQL, parameters);
        }

        public async Task<IEnumerable<T?>> QueryAsync<T>(string sQL, object parameters)
        {
            return await _sqlConnection.QueryAsync<T>(sQL, parameters);
        }
    }
}