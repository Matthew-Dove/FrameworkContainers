using ContainerExpressions.Containers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    public sealed class SqlResponse
    {
        internal SqlResponse() { }

        public Task<Response<T>> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReader(reader, usp, Sql.ConnectionString, parameters);
        }

        public async Task<Response<T>> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            var response = new Response<T>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteReader(reader, usp, connectionString, parameters);
                response = response.With(result);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    e.LogValue($"Aggregate error calling {usp} for type {typeof(T).FullName}: {e}");
                }
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {usp} for type {typeof(T).FullName}: {ex}");
            }

            return response;
        }

        public Task<Response<int>> ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(usp, Sql.ConnectionString, parameters);
        }

        public async Task<Response<int>> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            var response = new Response<int>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteNonQuery(usp, connectionString, parameters);
                response = response.With(result);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    e.LogValue($"Aggregate error calling {usp}: {e}");
                }
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {usp}: {ex}");
            }

            return response;
        }

        public Task<Response> BulkInsert(string tableName, DataTable dataTable)
        {
            return BulkInsert(tableName, dataTable, Sql.ConnectionString);
        }

        public async Task<Response> BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            var response = new Response();

            try
            {
                await StructuredQueryLanguage.BulkInsert(tableName, dataTable, connectionString);
                response = response.AsValid();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    e.LogValue($"Aggregate error calling {tableName}: {e}");
                }
            }
            catch (Exception ex)
            {
                ex.LogValue($"Error calling {tableName}: {ex}");
            }

            return response;
        }
    }
}
