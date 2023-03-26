namespace FrameworkContainers.Data
{
    public readonly struct SqlOptions
    {
        internal static SqlOptions Default => new SqlOptions(connectionString: null);

        /// <summary>The connection string to use.</summary>
        public string ConnectionString { get; }

        public SqlOptions(string connectionString = null)
        {
            ConnectionString = connectionString; // When null, then global default connection string will be selected.
        }
    }
}
