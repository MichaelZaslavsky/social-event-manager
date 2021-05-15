using System.Data;
using System.Data.SqlClient;

namespace SocialEventManager.DLL.Infrastructure
{
    public class SqlServerDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        public SqlServerDbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateDbConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}
