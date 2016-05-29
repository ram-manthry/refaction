using System;
using System.Data.SqlClient;

namespace refactor_me.DataAccess
{
    public static class SqlDataReaderExtensions
    {
        public static string GetString(this SqlDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                return reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception($"Expected column '{columnName}' not found");
            }
        }

        public static Guid GetGuid(this SqlDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                var guidStr = reader.GetString(ordinal);
                return new Guid(guidStr);
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception($"Expected column '{columnName}' not found");
            }
        }

        public static decimal GetDecimal(this SqlDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                return reader.GetDecimal(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception($"Expected column '{columnName}' not found");
            }
        }
    }
}