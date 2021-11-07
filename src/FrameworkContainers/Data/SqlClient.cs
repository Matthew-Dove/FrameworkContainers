using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    public interface ISqlClient
    {
        SqlMaybe Maybe { get; }
        SqlResponse Response { get; }
        Task<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters);
        Task<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters);
        Task<int> ExecuteNonQuery(string usp, params SqlParameter[] parameters);
        Task<int> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters);
        Task BulkInsert(string tableName, DataTable dataTable);
        Task BulkInsert(string tableName, DataTable dataTable, string connectionString);
    }

    public sealed class SqlClient : ISqlClient
    {
        public SqlMaybe Maybe => Sql.Maybe;
        public SqlResponse Response => Sql.Response;

        public Task<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, parameters);
        public Task<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, connectionString, parameters);

        public Task<int> ExecuteNonQuery(string usp, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, parameters);
        public Task<int> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, connectionString, parameters);

        public Task BulkInsert(string tableName, DataTable dataTable) => Sql.BulkInsert(tableName, dataTable);
        public Task BulkInsert(string tableName, DataTable dataTable, string connectionString) => Sql.BulkInsert(tableName, dataTable, connectionString);
    }
}
