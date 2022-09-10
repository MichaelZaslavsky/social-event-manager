using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Migrations;

public class DbMigrations
{
    private readonly IConfiguration _config;

    public DbMigrations(IConfiguration config)
    {
        _config = config;
    }

    public async Task Migrate(string environmentName)
    {
        IEnumerable<string> locations = GetLocations(environmentName);
        SqlConnection connection = new(_config.GetConnectionString(DbConstants.SocialEventManager));
        connection.Open();

        try
        {
            await new EnumLookupTableCreator(connection).Run();

            Evolve.Evolve evolve = new(connection, msg => Log.Information(msg))
            {
                Locations = locations,
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

    private static IEnumerable<string> GetLocations(string environmentName)
    {
        List<string> locations = new() { PathConstants.Migrations };

        // Exclude db/datasets from production and staging environments
        if (environmentName == Environments.Development)
        {
            locations.Add(PathConstants.DataSets);
        }

        return locations;
    }

    #endregion Private Methods
}
