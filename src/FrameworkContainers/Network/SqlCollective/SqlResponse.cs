﻿using ContainerExpressions.Containers;
using FrameworkContainers.Network.SqlCollective.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Network.SqlCollective
{
    public sealed class SqlResponse
    {
        internal static readonly SqlResponse Instance = new SqlResponse();

        private SqlResponse() { }

        public Response<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReader(reader, usp, SqlOptions.Default, parameters);
        }

        public Response<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            var response = new Response<T>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteReader(reader, usp, options, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogError($"Error calling sproc [{usp}] for type \"{typeof(T).FullName}\".");
            }

            return response;
        }

        public Response<int> ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(usp, SqlOptions.Default, parameters);
        }

        public Response<int> ExecuteNonQuery(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            var response = new Response<int>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteNonQuery(usp, options, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogError($"Error calling sproc [{usp}].");
            }

            return response;
        }

        public Response BulkInsert(string tableName, DataTable dataTable)
        {
            return BulkInsert(tableName, dataTable, SqlOptions.Default);
        }

        public Response BulkInsert(string tableName, DataTable dataTable, SqlOptions options)
        {
            var response = new Response();

            try
            {
                StructuredQueryLanguage.BulkInsert(tableName, dataTable, options);
                response = response.AsValid();
            }
            catch (Exception ex)
            {
                ex.LogError($"Error calling table [{tableName}].");
            }

            return response;
        }

        public Task<Response<T>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(reader, usp, SqlOptions.Default, parameters);
        }

        public async Task<Response<T>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            var response = new Response<T>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteReaderAsync(reader, usp, options, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogError($"Error calling sproc [{usp}] for type \"{typeof(T).FullName}\"");
            }

            return response;
        }

        public Task<Response<int>> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(usp, SqlOptions.Default, parameters);
        }

        public async Task<Response<int>> ExecuteNonQueryAsync(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            var response = new Response<int>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteNonQueryAsync(usp, options, parameters);
                response = response.With(result);
            }
            catch (Exception ex)
            {
                ex.LogError($"Error calling sproc [{usp}].");
            }

            return response;
        }

        public Task<Response> BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return BulkInsertAsync(tableName, dataTable, SqlOptions.Default);
        }

        public async Task<Response> BulkInsertAsync(string tableName, DataTable dataTable, SqlOptions options)
        {
            var response = new Response();

            try
            {
                await StructuredQueryLanguage.BulkInsertAsync(tableName, dataTable, options);
                response = response.AsValid();
            }
            catch (Exception ex)
            {
                ex.LogError($"Error calling table [{tableName}].");
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
            return SqlResponse.Instance.ExecuteReader(reader, usp, parameters);
        }

        public Response<T> ExecuteReader(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteReader(reader, usp, options, parameters);
        }

        public Task<Response<T>> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteReaderAsync(reader, usp, parameters);
        }

        public Task<Response<T>> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            return SqlResponse.Instance.ExecuteReaderAsync(reader, usp, options, parameters);
        }
    }
}
