using System.Diagnostics;

namespace SocialEventManager.Shared.Constants;

public static class ExceptionConstants
{
    public const string AnErrorOccurred = "An error occurred";
    public const string ExceptionWasA = "Exception was a";
    public const string NullReferenceException = $"{ExceptionWasA} null reference exception.";
    public const string ArgumentNullException = $"{ExceptionWasA} argument null exception.";
    public const string ArgumentException = $"{ExceptionWasA} argument exception.";
    public const string TimeoutException = $"{ExceptionWasA} timeout exception.";
    public const string SqlException = $"{ExceptionWasA} database exception.";
    public const string Exception = $"{ExceptionWasA} generic exception.";
    public const string CannotOpenDatabase = "cannot open database";
    public const string ANetworkRelated = "a network-related";
    public const string UnprocessableEntity = "Unprocessable Entity";
    public const string NotFound = "Not Found";
    public const string BadRequest = "Bad Request";
    public const string InternalServerError = "Internal Server Error";
    public const string DatabaseMigrationFailed = "Database migration failed.";
    public const string ConversionFailedFromStringToUniqueIdentifier = "Conversion failed when converting from a character string to uniqueidentifier.";
    public const string UniqueIdentifierIsIncompatibleWithInt = "Operand type clash: uniqueidentifier is incompatible with int";
    public const string InvalidConnectionString = "Format of the initialization string does not conform to specification starting at index 0.";
    public const string RoleNameIsInvalid = "\"Role name '' is invalid.(InvalidRoleName)\"";
    public const string ExceedMaximumAllowedLength = "String or binary data would be truncated";
    public const string ValueCannotBeNullOrEmpty = "Value cannot be null or empty.";
    public const string ValueCannotBeNullOrWhiteSpace = "Value cannot be null or whitespace.";
    public const string NoRecipientsHaveBeenSpecified = "No recipients have been specified.";
    public const string NoConnectionCouldBeMade = "No connection could be made because the target machine actively refused it.";
    public const string ConnectionRefused = "Connection refused";
    public const string CannotAssignRequestedAddress = "Cannot assign requested address";

    public static string CannotInsertTheValueNull(string columnName, string tableName, string databaseName = DbConstants.SocialEventManagerTest) =>
        $"Cannot insert the value NULL into column '{columnName}', table '{databaseName}.{tableName}'; " +
        $"column does not allow nulls. INSERT fails.{TheStatementHasBeenTerminatedWithSpace}";

    public static string ViolationOfPrimaryKeyConstraint(string partialConstraintName) =>
        $"{ViolationOfKeyConstraint("PRIMARY")} '{partialConstraintName}";

    public static string ViolationOfUniqueKeyConstraint(string constraintName, string tableName, string duplicateKeyValue) =>
        $"{ViolationOfKeyConstraint("UNIQUE")} '{constraintName}'. Cannot insert duplicate key in object '{tableName}'. " +
        $"The duplicate key value is ({duplicateKeyValue}).{TheStatementHasBeenTerminatedWithSpace}";

    public static string ValueCannotBeNull(string parameterName) => $"Value cannot be null. (Parameter '{parameterName}')";

    public static string MethodIsNotFound(string methodName, string className) => $"Method '{methodName}' is not found in '{className}' class.";

    public static string UnexpectedException(Exception ex) => $"Unexpected exception, {ex.Demystify()}";

    public static string ConfigurationKeyIsMissing(string key) => $"Configuration key '{key}` is missing.";

    private const string TheStatementHasBeenTerminatedWithSpace = "\r\nThe statement has been terminated.";

    private static string ViolationOfKeyConstraint(string keyType) =>
        $"Violation of {keyType} KEY constraint";
}
