using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Infrastructure.Migrations
{
    public class DbMigrations
    {
        private static IConfiguration _config;
        private static ILogger _logger;

        public DbMigrations(IConfiguration config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task Migrate(string environmentName)
        {
            string location = GetLocation(environmentName);
            var connection = new SqlConnection(_config.GetConnectionString(DbConstants.SocialEventManager));
            connection.Open();

            try
            {
                await new EnumLookupTableCreator(_logger, connection).Run();

                var evolve = new Evolve.Evolve(connection, msg => Log.Information(msg))
                {
                    Locations = new[] { location },
                    IsEraseDisabled = true,
                    MetadataTableSchema = SchemaConstants.Migration,
                    MetadataTableName = TableNameConstants.Changelog,
                };

                evolve.Migrate();
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ExceptionConstants.DatabaseMigrationFailed, ex);
                throw;
            }
        }

        #region Private Methods

        private static string GetLocation(string environmentName)
        {
            // exclude db/datasets from production and staging environments
            return environmentName == Environments.Production || environmentName == Environments.Staging
                ? PathConstants.Migrations
                : PathConstants.Db;
        }

        #endregion Private Methods
    }
}
