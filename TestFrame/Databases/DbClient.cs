using Dapper;
using System.Data;

namespace TestFrame.Databases
{
    public class DbClient : IDbClient
    {
        public T GetOneRecordFromDatabase<T>(IDbConnection dbConnection, string query)
        {
            T result;
            using (dbConnection)
            {
                result = dbConnection.QuerySingleOrDefault<T>(query);
            }

            return result;
        }

        public List<T> GetRecordsFromDatabase<T>(IDbConnection dbConnection, string query)
        {
            List<T> result;
            using (dbConnection)
            {
                result = dbConnection.Query<T>(query).ToList();

            }
            return result;
        }

        public async Task AddRecordToDatabase(IDbConnection dbConnection, string query, Dictionary<string, object> parameters)
        {
            using (dbConnection)
            {
                var result = await dbConnection.QueryAsync(query, parameters);
            }
        }

        public async Task<T> GetOneRecordFromDatabaseAsync<T>(IDbConnection dbConnection, string query, Dictionary<string, object> parameters)
        {
            T result;

            using (dbConnection)
            {
                result = await dbConnection.QuerySingleOrDefaultAsync<T>(query, parameters);
            }
            return result;
        }

        public async Task DeleteRecordFromDatabaseAsync(IDbConnection dbConnection, Dictionary<string, object> parameters, string query)
        {
            using (dbConnection)
            {
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
    }
}
