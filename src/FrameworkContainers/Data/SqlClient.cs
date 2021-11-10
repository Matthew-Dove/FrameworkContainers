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

        T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters);
        T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters);
        int ExecuteNonQuery(string usp, params SqlParameter[] parameters);
        int ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters);
        void BulkInsert(string tableName, DataTable dataTable);
        void BulkInsert(string tableName, DataTable dataTable, string connectionString);

        Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters);
        Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters);
        Task BulkInsertAsync(string tableName, DataTable dataTable);
        Task BulkInsertAsync(string tableName, DataTable dataTable, string connectionString);
    }

    public sealed class SqlClient : ISqlClient
    {
        public SqlMaybe Maybe => Sql.Maybe;
        public SqlResponse Response => Sql.Response;

        public T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, parameters);
        public T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, connectionString, parameters);

        public int ExecuteNonQuery(string usp, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, parameters);
        public int ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, connectionString, parameters);

        public void BulkInsert(string tableName, DataTable dataTable) => Sql.BulkInsert(tableName, dataTable);
        public void BulkInsert(string tableName, DataTable dataTable, string connectionString) => Sql.BulkInsert(tableName, dataTable, connectionString);

        public Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters) => Sql.ExecuteReaderAsync(reader, usp, parameters);
        public Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters) => Sql.ExecuteReaderAsync(reader, usp, connectionString, parameters);

        public Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters) => Sql.ExecuteNonQueryAsync(usp, parameters);
        public Task<int> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters) => Sql.ExecuteNonQueryAsync(usp, connectionString, parameters);

        public Task BulkInsertAsync(string tableName, DataTable dataTable) => Sql.BulkInsertAsync(tableName, dataTable);
        public Task BulkInsertAsync(string tableName, DataTable dataTable, string connectionString) => Sql.BulkInsertAsync(tableName, dataTable, connectionString);
    }
}
