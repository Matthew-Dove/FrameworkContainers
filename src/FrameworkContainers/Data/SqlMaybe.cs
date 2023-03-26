using ContainerExpressions.Containers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    public sealed class SqlMaybe
    {
        internal static readonly SqlMaybe Instance = new SqlMaybe();

        private SqlMaybe() { }

        public Maybe<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReader(reader, usp, Sql.ConnectionString, parameters);
        }

        public Maybe<T> ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<T>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteReader(reader, usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(ex);
            }

            return maybe;
        }

        public Maybe<int> ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(usp, Sql.ConnectionString, parameters);
        }

        public Maybe<int> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<int>();

            try
            {
                var result = StructuredQueryLanguage.ExecuteNonQuery(usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(ex);
            }

            return maybe;
        }

        public Maybe<bool> BulkInsert(string tableName, DataTable dataTable)
        {
            return BulkInsert(tableName, dataTable, Sql.ConnectionString);
        }

        public Maybe<bool> BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            var maybe = new Maybe<bool>();

            try
            {
                StructuredQueryLanguage.BulkInsert(tableName, dataTable, connectionString);
                maybe = maybe.With(true);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(ex);
            }

            return maybe;
        }

        public Task<Maybe<T>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(reader, usp, Sql.ConnectionString, parameters);
        }

        public async Task<Maybe<T>> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<T>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteReaderAsync(reader, usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(ex);
            }

            return maybe;
        }

        public Task<Maybe<int>> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(usp, Sql.ConnectionString, parameters);
        }

        public async Task<Maybe<int>> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters)
        {
            var maybe = new Maybe<int>();

            try
            {
                var result = await StructuredQueryLanguage.ExecuteNonQueryAsync(usp, connectionString, parameters);
                maybe = maybe.With(result);
            }
            catch (Exception ex)
            {
                maybe = maybe.With(ex);
            }

            return maybe;
        }

        public Task<Maybe<bool>> BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return BulkInsertAsync(tableName, dataTable, Sql.ConnectionString);
        }

        public async Task<Maybe<bool>> BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            var maybe = new Maybe<bool>();

            try
            {
                await StructuredQueryLanguage.BulkInsertAsync(tableName, dataTable, connectionString);
                maybe = maybe.With(true);
            }
            catch (Exception ex)
            {
                maybe.With(ex);
            }

            return maybe;
        }
    }

    public sealed class SqlMaybe<T>
    {
        internal static readonly SqlMaybe<T> Instance = new SqlMaybe<T>();

        private SqlMaybe() { }

        public Maybe<T> ExecuteReader(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteReader(reader, usp, parameters);
        }

        public Maybe<T> ExecuteReader(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteReader<T>(reader, usp, connectionString, parameters);
        }

        public Maybe<int> ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteNonQuery(usp, parameters);
        }

        public Maybe<int> ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteNonQuery(usp, connectionString, parameters);
        }

        public Maybe<bool> BulkInsert(string tableName, DataTable dataTable)
        {
            return SqlMaybe.Instance.BulkInsert(tableName, dataTable);
        }

        public Maybe<bool> BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            return SqlMaybe.Instance.BulkInsert(tableName, dataTable, connectionString);
        }

        public Task<Maybe<T>> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteReaderAsync(reader, usp, parameters);
        }

        public Task<Maybe<T>> ExecuteReaderAsync(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteReaderAsync(reader, usp, connectionString, parameters);
        }

        public Task<Maybe<int>> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteNonQueryAsync(usp, parameters);
        }

        public Task<Maybe<int>> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters)
        {
            return SqlMaybe.Instance.ExecuteNonQueryAsync(usp, connectionString, parameters);
        }

        public Task<Maybe<bool>> BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return SqlMaybe.Instance.BulkInsertAsync(tableName, dataTable);
        }

        public Task<Maybe<bool>> BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            return SqlMaybe.Instance.BulkInsertAsync(tableName, dataTable, connectionString);
        }
    }
}
