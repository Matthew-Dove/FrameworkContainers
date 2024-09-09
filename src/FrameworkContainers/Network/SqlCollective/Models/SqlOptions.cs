using ContainerExpressions.Containers;
using FrameworkContainers.Models;

namespace FrameworkContainers.Network.SqlCollective.Models
{
    public readonly struct SqlOptions
    {
        internal static readonly SqlOptions Default = new SqlOptions(connectionString: null, commandTimeoutSeconds: default);

        /// <summary>The connection string to use.</summary>
        internal string ConnectionString { get; }

        /// <summary>The time to wait (in seconds) before giving up on a SQL command (i.e. not the timeout from the connection string).</summary>
        internal int CommandTimeoutSeconds { get; }

        public SqlOptions(string connectionString = null, int commandTimeoutSeconds = default)
        {
            var seconds = commandTimeoutSeconds.ThrowIfLessThan(default).ThrowIfGreaterThan(Constants.Sql.MAX_COMMAND_TIMEOUT_SECONDS);

            ConnectionString = connectionString; // When null, then the global default connection string will be selected (from the Sql class).
            CommandTimeoutSeconds = seconds == default ? Constants.Sql.COMMAND_TIMEOUT_SECONDS : seconds;
        }

        public static implicit operator SqlOptions(string connectionString) => new SqlOptions(connectionString: connectionString);
        public static implicit operator SqlOptions(int timeoutSeconds) => new SqlOptions(commandTimeoutSeconds: timeoutSeconds);
    }
}
