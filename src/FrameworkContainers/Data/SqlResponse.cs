﻿using ContainerExpressions.Containers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    public sealed class SqlResponse
    {
        internal SqlResponse() { }

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