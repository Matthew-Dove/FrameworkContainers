using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FrameworkContainers.Data
{
    public static class Sql
    {
        /// <summary>The connection string to use when not specified.</summary>
        public static string ConnectionString = null;

        public static readonly SqlResponse Response = SqlResponse.Instance;

        public static readonly SqlMaybe Maybe = SqlMaybe.Instance;

        public static T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReader(reader, usp, ConnectionString, parameters);
        }

        public static T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteReader(reader, usp, connectionString, parameters);
        }

        public static int ExecuteNonQuery(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(usp, ConnectionString, parameters);
        }

        public static int ExecuteNonQuery(string usp, string connectionString, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteNonQuery(usp, connectionString, parameters);
        }

        public static void BulkInsert(string tableName, DataTable dataTable)
        {
            BulkInsert(tableName, dataTable, ConnectionString);
        }

        public static void BulkInsert(string tableName, DataTable dataTable, string connectionString)
        {
            StructuredQueryLanguage.BulkInsert(tableName, dataTable, connectionString);
        }

        public static Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsync(reader, usp, ConnectionString, parameters);
        }

        public static Task<T> ExecuteReaderAsync<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteReaderAsync(reader, usp, connectionString, parameters);
        }

        public static Task<int> ExecuteNonQueryAsync(string usp, params SqlParameter[] parameters)
        {
            return ExecuteNonQueryAsync(usp, ConnectionString, parameters);
        }

        public static Task<int> ExecuteNonQueryAsync(string usp, string connectionString, params SqlParameter[] parameters)
        {
            return StructuredQueryLanguage.ExecuteNonQueryAsync(usp, connectionString, parameters);
        }

        public static Task BulkInsertAsync(string tableName, DataTable dataTable)
        {
            return BulkInsertAsync(tableName, dataTable, ConnectionString);
        }

        public static Task BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            return StructuredQueryLanguage.BulkInsertAsync(tableName, dataTable, connectionString);
        }
    }

    internal static class StructuredQueryLanguage
    {
        public static T ExecuteReader<T>(Func<IDataReader, T> reader, string usp, string connectionString, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
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
            using (var connection = new SqlConnection(connectionString))
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
            using (var connection = new SqlConnection(connectionString))
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
            using (var connection = new SqlConnection(connectionString))
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
            var bulkCopy = new SqlBulkCopy(connectionString) { DestinationTableName = tableName };
            try
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    bulkCopy.ColumnMappings.Add(columnName, columnName);
                }
                bulkCopy.WriteToServer(dataTable);
            }
            finally { bulkCopy?.Close(); }
        }

        public static async Task BulkInsertAsync(string tableName, DataTable dataTable, string connectionString)
        {
            var bulkCopy = new SqlBulkCopy(connectionString) { DestinationTableName = tableName };
            try
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    bulkCopy.ColumnMappings.Add(columnName, columnName);
                }
                await bulkCopy.WriteToServerAsync(dataTable);
            }
            finally { bulkCopy?.Close(); }
        }
    }

    public static class DataReaderExtensions
    {
        public static T Get<T>(this IDataReader dr, string fieldName)
        {
            T result = default;
            object cell = dr[fieldName];

            if (cell != DBNull.Value)
            {
                Type type = typeof(T);

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = type.GetGenericArguments()[0];
                }

                if (type.IsEnum)
                {
                    string value = cell.ToString();
                    result = (T)(int.TryParse(value, out int number) ? Enum.ToObject(type, number) : Enum.Parse(type, value));
                }
                else
                {
                    result = (T)Convert.ChangeType(cell, type);
                }
            }

            return result;
        }
    }
}
