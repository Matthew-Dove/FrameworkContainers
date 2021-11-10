﻿using ContainerExpressions.Containers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    public sealed class SqlMaybe
    {
        internal SqlMaybe() { }

        public Maybe<T, Exception[]> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReader(reader, usp, Sql.ConnectionString, parameters);
        }

        public Maybe<T, Exception[]> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<T, Exception[]>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteReader(reader, usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (Exception ex)
            {
                maybe.With(new Exception[] { ex });
            }

            return maybe;
        }

        public Maybe<int, Exception[]> ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(usp, Sql.ConnectionString, parameters);
        }

        public Maybe<int, Exception[]> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<int, Exception[]>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteNonQuery(usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (Exception ex)
            {
                maybe.With(new Exception[] { ex });
            }

            return maybe;
        }

        public Maybe<bool, Exception[]> BulkInsert(string tableName, DataTable dataTable)
        {
            return BulkInsert(tableName, dataTable, Sql.ConnectionString);
        }

        public Maybe<bool, Exception[]> BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            var maybe = new Maybe<bool, Exception[]>();

            try
            {
                StructuredQueryLanguage.BulkInsert(tableName, dataTable, connectionString);
                maybe = maybe.With(true);
            }
            catch (Exception ex)
            {
                maybe.With(new Exception[] { ex });
            }

            return maybe;
        }

        public Task<Maybe<T, Exception[]>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(reader, usp, Sql.ConnectionString, parameters);
        }

        public async Task<Maybe<T, Exception[]>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<T, Exception[]>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteReaderAsync(reader, usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (AggregateException ae)
            {
                maybe = maybe.With(ae.Flatten().InnerExceptions.ToArray());
            }
            catch (Exception ex)
            {
                maybe.With(new Exception[] { ex });
            }

            return maybe;
        }

        public Task<Maybe<int, Exception[]>> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(usp, Sql.ConnectionString, parameters);
        }

        public async Task<Maybe<int, Exception[]>> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<int, Exception[]>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteNonQueryAsync(usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (AggregateException ae)
            {
                maybe = maybe.With(ae.Flatten().InnerExceptions.ToArray());
            }
            catch (Exception ex)
            {
                maybe.With(new Exception[] { ex });
            }

            return maybe;
        }

        public Task<Maybe<bool, Exception[]>> BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return BulkInsertAsync(tableName, dataTable, Sql.ConnectionString);
        }

        public async Task<Maybe<bool, Exception[]>> BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            var maybe = new Maybe<bool, Exception[]>();

            try
            {
                await StructuredQueryLanguage.BulkInsertAsync(tableName, dataTable, connectionString);
                maybe = maybe.With(true);
            }
            catch (AggregateException ae)
            {
                maybe = maybe.With(ae.Flatten().InnerExceptions.ToArray());
            }
            catch (Exception ex)
            {
                maybe.With(new Exception[] { ex });
            }

            return maybe;
        }
    }
}
