namespace FrameworkContainers.Network.SqlCollective.Models
{
    public readonly struct SqlOptions
    {
        internal static SqlOptions Default => new SqlOptions(connectionString: null, timeoutSeconds: default);

        /// <summary>The connection string to use.</summary>
        public string ConnectionString { get; }

        /// <summary>The time to wait (in seconds) before giving up on a SQL command.</summary>
        public int TimeoutSeconds { get; }

        public SqlOptions(string connectionString = null, int timeoutSeconds = default)
        {
            if (timeoutSeconds < 0 || timeoutSeconds >= 14400) ArgumentOutOfRangeException(nameof(timeoutSeconds));

            ConnectionString = connectionString; // When null, then the global default connection string will be selected (from the Sql class).
            TimeoutSeconds = timeoutSeconds;
        }

        public static implicit operator SqlOptions(string connectionString) => new SqlOptions(connectionString: connectionString);
        public static implicit operator SqlOptions(int timeoutSeconds) => new SqlOptions(timeoutSeconds: timeoutSeconds);
    }
}
