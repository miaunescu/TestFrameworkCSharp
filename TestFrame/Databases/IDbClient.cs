using System.Data;

namespace TestFrame.Databases
{
    public interface IDbClient
    {
        T GetOneRecordFromDatabase<T>(IDbConnection dbConnection, string query);
        Task<T> GetOneRecordFromDatabaseAsync<T>(IDbConnection dbConnection, string query, Dictionary<string, object> parameters);
        Task DeleteRecordFromDatabaseAsync(IDbConnection dbConnection, Dictionary<string, object> parameters, string query);
        List<T> GetRecordsFromDatabase<T>(IDbConnection dbConnection, string query);
        Task AddRecordToDatabase(IDbConnection dbConnection, string query, Dictionary<string, object> parameters);
    }
}
