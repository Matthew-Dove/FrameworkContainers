using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using FrameworkContainers.Network.SqlCollective.Models;

namespace FrameworkContainers.Network.SqlCollective
{
    internal static class StructuredQueryLanguage
    {
        public static T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(options.ConnectionString ?? Sql.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandTimeout = options.CommandTimeoutSeconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    using (var dr = command.ExecuteReader())
                    {
                        return reader(dr);
                    }
                }
            }
        }

        public static async Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(options.ConnectionString ?? Sql.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandTimeout = options.CommandTimeoutSeconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        return reader(dr);
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(options.ConnectionString ?? Sql.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandTimeout = options.CommandTimeoutSeconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public static async Task<int> ExecuteNonQueryAsync(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(options.ConnectionString ?? Sql.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandTimeout = options.CommandTimeoutSeconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static T ExecuteScalar<T>(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(options.ConnectionString ?? Sql.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandTimeout = options.CommandTimeoutSeconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    var scalar = command.ExecuteScalar();
                    if (scalar is null || scalar == DBNull.Value) return default;
                    return (T)Convert.ChangeType(scalar, typeof(T));
                }
            }
        }

        public static async Task<T> ExecuteScalarAsync<T>(string usp, SqlOptions options, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(options.ConnectionString ?? Sql.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandTimeout = options.CommandTimeoutSeconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    var scalar = await command.ExecuteScalarAsync();
                    if (scalar is null || scalar == DBNull.Value) return default;
                    return (T)Convert.ChangeType(scalar, typeof(T));
                }
            }
        }

        public static void BulkInsert(string tableName, DataTable dataTable, SqlOptions options)
        {
            using (var bulkCopy = new SqlBulkCopy(options.ConnectionString ?? Sql.ConnectionString))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = options.CommandTimeoutSeconds;
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    bulkCopy.ColumnMappings.Add(columnName, columnName);
                }
                bulkCopy.WriteToServer(dataTable);
            }
        }

        public static async Task BulkInsertAsync(string tableName, DataTable dataTable, SqlOptions options)
        {
            using (var bulkCopy = new SqlBulkCopy(options.ConnectionString ?? Sql.ConnectionString))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = options.CommandTimeoutSeconds;
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    bulkCopy.ColumnMappings.Add(columnName, columnName);
                }
                await bulkCopy.WriteToServerAsync(dataTable);
            }
        }
    }
}
