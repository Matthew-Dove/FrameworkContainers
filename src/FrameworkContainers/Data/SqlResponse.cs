using ContainerExpressions.Containers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    public sealed class SqlResponse
    {
        internal static readonly SqlResponse Instance = new SqlResponse();

        private SqlResponse() { }

        public Response<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReader(reader, usp, Sql.ConnectionString, parameters);
        }

        public Response<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            var response = new Response<T>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteReader(reader, usp, connectionString, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {usp} for type {typeof(T).FullName}: {ex}");
            }

            return response;
        }

        public Response<int> ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(usp, Sql.ConnectionString, parameters);
        }

        public Response<int> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            var response = new Response<int>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteNonQuery(usp, connectionString, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {usp}: {ex}");
            }

            return response;
        }

        public Response BulkInsert(string tableName, DataTable dataTable)
        {
            return BulkInsert(tableName, dataTable, Sql.ConnectionString);
        }

        public Response BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            var response = new Response();

            try
            {
                StructuredQueryLanguage.BulkInsert(tableName, dataTable, connectionString);
                response = response.AsValid();
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {tableName}: {ex}");
            }

            return response;
        }

        public Task<Response<T>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(reader, usp, Sql.ConnectionString, parameters);
        }

        public async Task<Response<T>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            var response = new Response<T>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteReaderAsync(reader, usp, connectionString, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {usp} for type {typeof(T).FullName}: {ex}");
            }

            return response;
        }

        public Task<Response<int>> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(usp, Sql.ConnectionString, parameters);
        }

        public async Task<Response<int>> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters)
        {
            var response = new Response<int>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteNonQueryAsync(usp, connectionString, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {usp}: {ex}");
            }

            return response;
        }

        public Task<Response> BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return BulkInsertAsync(tableName, dataTable, Sql.ConnectionString);
        }

        public async Task<Response> BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            var response = new Response();

            try
            {
                await StructuredQueryLanguage.BulkInsertAsync(tableName, dataTable, connectionString);
                response = response.AsValid();
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {tableName}: {ex}");
            }

            return response;
        }
    }

    public sealed class SqlResponse<T>
    {
        internal static readonly SqlResponse<T> Instance = new SqlResponse<T>();

        private SqlResponse() { }

        public Response<T> ExecuteReader(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteReader<T>(reader, usp, parameters);
        }

        public Response<T> ExecuteReader(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteReader<T>(reader, usp, connectionString, parameters);
        }

        public Response<int> ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteNonQuery(usp, parameters);
        }

        public Response<int> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteNonQuery(usp, connectionString, parameters);
        }

        public Response BulkInsert(string tableName, DataTable dataTable)
        {
            return SqlResponse.Instance.BulkInsert(tableName, dataTable);
        }

        public Response BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            return SqlResponse.Instance.BulkInsert(tableName, dataTable, connectionString);
        }

        public Task<Response<T>> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteReaderAsync<T>(reader, usp, parameters);
        }

        public Task<Response<T>> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteReaderAsync<T>(reader, usp, connectionString, parameters);
        }

        public Task<Response<int>> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteNonQueryAsync(usp, parameters);
        }

        public Task<Response<int>> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteNonQueryAsync(usp, connectionString, parameters);
        }

        public Task<Response> BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return SqlResponse.Instance.BulkInsertAsync(tableName, dataTable);
        }

        public Task<Response> BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            return SqlResponse.Instance.BulkInsertAsync(tableName, dataTable, connectionString);
        }
    }
}
