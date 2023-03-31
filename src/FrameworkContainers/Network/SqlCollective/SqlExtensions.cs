using System.Data;
using System;

namespace FrameworkContainers.Network.SqlCollective
{
    public static class SqlExtensions
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
