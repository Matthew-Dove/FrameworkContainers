using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;

namespace FrameworkContainers.Network.SqlCollective
{
    internal static class StructuredQueryLanguage
    {
        public static T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString ?? Sql.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    using (var dr = command.ExecuteReader())
                    {
                        return reader(dr);
                    }
                }
            }
        }

        public static async Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString ?? Sql.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        return reader(dr);
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString ?? Sql.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public static async Task<int> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString ?? Sql.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(usp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static void BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            using (var bulkCopy = new SqlBulkCopy(connectionString ?? Sql.ConnectionString))
            {
                bulkCopy.DestinationTableName = tableName;
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    bulkCopy.ColumnMappings.Add(columnName, columnName);
                }
                bulkCopy.WriteToServer(dataTable);
            }
        }

        public static async Task BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            using (var bulkCopy = new SqlBulkCopy(connectionString ?? Sql.ConnectionString))
            {
                bulkCopy.DestinationTableName = tableName;
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
