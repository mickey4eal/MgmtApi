namespace ManagementApi.Services.Interfaces
{
    public interface ISqlConnection
    {
        Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object parameters);
        Task<IEnumerable<T?>> QueryAsync<T>(string sQL, object parameters);
    }
}
