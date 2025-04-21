using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using FrameworkContainers.Network.SqlCollective.Models;

namespace FrameworkContainers.Network.SqlCollective
{
    public static class Sql
    {
        public static readonly SqlResponse Response = SqlResponse.Instance;
        public static readonly SqlMaybe Maybe = SqlMaybe.Instance;

        /// <summary>The connection string to use when not specified.</summary>
        public static string ConnectionString = null;

        public static T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReader(reader, usp, SqlOptions.Default, parameters);
        }

        public static T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteReader(reader, usp, options, parameters);
        }

        public static int ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(usp, SqlOptions.Default, parameters);
        }

        public static int ExecuteNonQuery(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteNonQuery(usp, options, parameters);
        }

        public static T ExecuteScalar<T>(string usp, params SqlParameter[] parameters)
        {
            return ExecuteScalar<T>(usp, SqlOptions.Default, parameters);
        }

        public static T ExecuteScalar<T>(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteScalar<T>(usp, options, parameters);
        }

        public static void BulkInsert(string tableName, DataTable dataTable)
        {
            BulkInsert(tableName, dataTable, SqlOptions.Default);
        }

        public static void BulkInsert(string tableName, DataTable dataTable, SqlOptions options)
        {
            StructuredQueryLanguage.BulkInsert(tableName, dataTable, options);
        }

        public static Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(reader, usp, SqlOptions.Default, parameters);
        }

        public static Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteReaderAsync(reader, usp, options, parameters);
        }

        public static Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(usp, SqlOptions.Default, parameters);
        }

        public static Task<int> ExecuteNonQueryAsync(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteNonQueryAsync(usp, options, parameters);
        }

        public static Task<T> ExecuteScalarAsync<T>(string usp, params SqlParameter[] parameters)
        {
            return ExecuteScalarAsync<T>(usp, SqlOptions.Default, parameters);
        }

        public static Task<T> ExecuteScalarAsync<T>(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteScalarAsync<T>(usp, options, parameters);
        }

        public static Task BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return BulkInsertAsync(tableName, dataTable, SqlOptions.Default);
        }

        public static Task BulkInsertAsync(string tableName, DataTable dataTable, SqlOptions options)
        {
            return StructuredQueryLanguage.BulkInsertAsync(tableName, dataTable, options);
        }
    }

    public static class Sql<T>
    {
        public static readonly SqlClient<T> Client = new SqlClient<T>();
    }
}
