using System;
using System.Data;
using System.Data.SqlClient;

namespace SocialEventManager.DAL.Infrastructure
{
    public sealed class DbSession : IDbSession, IDisposable
    {
        public DbSession(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; set; }

        public void Dispose() => Connection?.Dispose();
    }
}
