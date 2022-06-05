using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Infrastructure.Migrations;

public class DbMigrations
{
    private readonly IConfiguration _config;

    public DbMigrations(IConfiguration config)
    {
        _config = config;
    }

    public async Task Migrate(string environmentName)
    {
        string location = GetLocation(environmentName);
        SqlConnection connection = new(_config.GetConnectionString(DbConstants.SocialEventManager));
        connection.Open();

        try
        {
            await new EnumLookupTableCreator(connection).Run();

            Evolve.Evolve evolve = new(connection, msg => Log.Information(msg))
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
            Log.Error(ExceptionConstants.DatabaseMigrationFailed, ex);
            throw;
        }
    }

    #region Private Methods

    private static string GetLocation(string environmentName)
    {
        // Exclude db/datasets from production and staging environments
        return environmentName == Environments.Production || environmentName == Environments.Staging
            ? PathConstants.Migrations
            : PathConstants.Db;
    }

    #endregion Private Methods
}
