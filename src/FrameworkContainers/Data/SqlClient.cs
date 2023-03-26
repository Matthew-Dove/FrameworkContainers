using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    /// <summary>Dependency inversion alterative to the static class.</summary>
    public interface ISqlClient
    {
        SqlMaybe Maybe { get; }
        SqlResponse Response { get; }

        T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters);
        T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters);
        int ExecuteNonQuery(string usp, params SqlParameter[] parameters);
        int ExecuteNonQuery(string usp, SqlOptions options, params SqlParameter[] parameters);
        void BulkInsert(string tableName, DataTable dataTable);
        void BulkInsert(string tableName, DataTable dataTable, SqlOptions options);

        Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters);
        Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string usp, SqlOptions options, params SqlParameter[] parameters);
        Task BulkInsertAsync(string tableName, DataTable dataTable);
        Task BulkInsertAsync(string tableName, DataTable dataTable, SqlOptions options);
    }

    public sealed class SqlClient : ISqlClient
    {
        public SqlMaybe Maybe => Sql.Maybe;
        public SqlResponse Response => Sql.Response;

        public T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, parameters);
        public T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, options, parameters);

        public int ExecuteNonQuery(string usp, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, parameters);
        public int ExecuteNonQuery(string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, options, parameters);

        public void BulkInsert(string tableName, DataTable dataTable) => Sql.BulkInsert(tableName, dataTable);
        public void BulkInsert(string tableName, DataTable dataTable, SqlOptions options) => Sql.BulkInsert(tableName, dataTable, options);

        public Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters) => Sql.ExecuteReaderAsync(reader, usp, parameters);
        public Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteReaderAsync(reader, usp, options, parameters);

        public Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters) => Sql.ExecuteNonQueryAsync(usp, parameters);
        public Task<int> ExecuteNonQueryAsync(string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteNonQueryAsync(usp, options, parameters);

        public Task BulkInsertAsync(string tableName, DataTable dataTable) => Sql.BulkInsertAsync(tableName, dataTable);
        public Task BulkInsertAsync(string tableName, DataTable dataTable, SqlOptions options) => Sql.BulkInsertAsync(tableName, dataTable, options);
    }

    /// <summary>Dependency inversion alterative to the static class (for a single type).</summary>
    public interface ISqlClient<T>
    {
        SqlMaybe<T> Maybe { get; }
        SqlResponse<T> Response { get; }

        T ExecuteReader(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters);
        T ExecuteReader(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters);
        int ExecuteNonQuery(string usp, params SqlParameter[] parameters);
        int ExecuteNonQuery(string usp, SqlOptions options, params SqlParameter[] parameters);
        void BulkInsert(string tableName, DataTable dataTable);
        void BulkInsert(string tableName, DataTable dataTable, SqlOptions options);

        Task<T> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters);
        Task<T> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string usp, SqlOptions options, params SqlParameter[] parameters);
        Task BulkInsertAsync(string tableName, DataTable dataTable);
        Task BulkInsertAsync(string tableName, DataTable dataTable, SqlOptions options);
    }

    public sealed class SqlClient<T> : ISqlClient<T>
    {
        public SqlMaybe<T> Maybe => SqlMaybe<T>.Instance;
        public SqlResponse<T> Response => SqlResponse<T>.Instance;

        public T ExecuteReader(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, parameters);
        public T ExecuteReader(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteReader(reader, usp, options, parameters);

        public int ExecuteNonQuery(string usp, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, parameters);
        public int ExecuteNonQuery(string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteNonQuery(usp, options, parameters);

        public void BulkInsert(string tableName, DataTable dataTable) => Sql.BulkInsert(tableName, dataTable);
        public void BulkInsert(string tableName, DataTable dataTable, SqlOptions options) => Sql.BulkInsert(tableName, dataTable, options);

        public Task<T> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters) => Sql.ExecuteReaderAsync(reader, usp, parameters);
        public Task<T> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteReaderAsync(reader, usp, options, parameters);

        public Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters) => Sql.ExecuteNonQueryAsync(usp, parameters);
        public Task<int> ExecuteNonQueryAsync(string usp, SqlOptions options, params SqlParameter[] parameters) => Sql.ExecuteNonQueryAsync(usp, options, parameters);

        public Task BulkInsertAsync(string tableName, DataTable dataTable) => Sql.BulkInsertAsync(tableName, dataTable);
        public Task BulkInsertAsync(string tableName, DataTable dataTable, SqlOptions options) => Sql.BulkInsertAsync(tableName, dataTable, options);
    }
}
