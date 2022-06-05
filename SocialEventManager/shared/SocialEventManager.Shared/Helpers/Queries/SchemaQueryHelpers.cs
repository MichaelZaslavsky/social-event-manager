namespace SocialEventManager.Shared.Helpers.Queries;

public static class SchemaQueryHelpers
{
    public static string CreateSchemaIfNotExists(string schemaName)
    {
        return $@"
                IF NOT EXISTS
                (
                    SELECT  TOP 1 1
                    FROM    sys.schemas
                    WHERE   [name] = '{schemaName}'
                )
                BEGIN
                    EXECUTE('CREATE SCHEMA {schemaName}');
                    SELECT 1;
                END
                ELSE
                BEGIN
                    SELECT 0;
                END";
    }
}
