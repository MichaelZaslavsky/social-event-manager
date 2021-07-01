using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Helpers.Queries
{
    public static class TableQueryHelpers
    {
        public static string RecreateType(string schemaName, string typeName)
        {
            return $@"
                IF EXISTS
                (
                    SELECT  TOP 1 1
                    FROM    sys.types t
                            INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                    WHERE   t.[name] = '{typeName}'
                            AND s.[name] = '{schemaName}'
                )
                BEGIN
	                DROP TYPE {schemaName}.{typeName};
                END

                CREATE TYPE {schemaName}.{typeName} AS TABLE
                (
	                ID		    INT NOT NULL,
	                Name	    NVARCHAR({LengthConstants.Length100}) NOT NULL,
	                Description	NVARCHAR({LengthConstants.Length255}) NULL
                );";
        }

        public static string GetTableNames(string schemaName)
        {
            return $@"
                SELECT  TABLE_NAME
                FROM    INFORMATION_SCHEMA.TABLES
                WHERE   TABLE_SCHEMA = '{schemaName}';";
        }

        public static string CreateBasicTable(string schemaName, string tableName)
        {
            return $@"
                CREATE TABLE {schemaName}.{tableName}
                (
                    Id          INT NOT NULL CONSTRAINT PK_{tableName} PRIMARY KEY CLUSTERED,
                    Name        NVARCHAR({LengthConstants.Length100}) NOT NULL,
                    Description NVARCHAR({LengthConstants.Length255}) NULL
                );";
        }

        public static string SafelyDropAllTables()
        {
            return @"DECLARE @sql NVARCHAR(MAX) = N'';
                SELECT @sql += N'
                ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
                    + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) +
                    ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
                FROM sys.foreign_keys;

                EXEC sp_executesql @sql;

                EXEC sp_MSforeachtable 'DROP TABLE ?'";
        }
    }
}
