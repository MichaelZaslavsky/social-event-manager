using System;
using System.Data;
using System.Data.SqlClient;

namespace SocialEventManager.DAL.Infrastructure
{
    public sealed class DbSession : IDbSession, IDisposable
    {
        private readonly string _connectionString;

        public DbSession(string connectionString)
        {
            _connectionString = connectionString;

            Connection = new SqlConnection(_connectionString);
            Connection.Open();
        }

        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; set; }

        public void Dispose() => Connection?.Dispose();
    }
}
