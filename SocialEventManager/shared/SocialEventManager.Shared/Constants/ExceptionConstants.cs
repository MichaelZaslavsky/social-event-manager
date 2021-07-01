using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.Constants
{
    public static class ExceptionConstants
    {
        public const string ValueCannotBeNull = "Value cannot be null. (Parameter 'source')";
        public const string AnErrorOccurred = "An error occurred";
        public const string ExceptionWasA = "Exception was a ";
        public const string NullReferenceException = ExceptionWasA + "null reference exception.";
        public const string ArgumentNullException = ExceptionWasA + "argument null exception.";
        public const string ArgumentException = ExceptionWasA + "argument exception.";
        public const string TimeoutException = ExceptionWasA + "timeout exception.";
        public const string SqlException = ExceptionWasA + "database exception.";
        public const string Exception = ExceptionWasA + "generic exception.";
        public const string CannotOpenDatabase = "cannot open database";
        public const string ANetworkRelated = "a network-related";
        public const string NotFound = "Not Found";
        public const string BadRequest = "Bad Request";
        public const string InternalServerError = "Internal Server Error";
        public const string DatabaseMigrationFailed = "Database migration failed.";
        public const string ConversionFailedFromStringToUniqueIdentifier = "Conversion failed when converting from a character string to uniqueidentifier.";
        public const string UniqueIdentifierIsIncompatibleWithInt = "Operand type clash: uniqueidentifier is incompatible with int";

        public static string ForeignKeyConstraintConflict(string foriegnKeyName, string tableName, string columnName, string dbName = DbConstants.SocialEventManagerTest) =>
            $"The INSERT statement conflicted with the FOREIGN KEY constraint \"{foriegnKeyName}\". " +
            $"The conflict occurred in database \"{dbName}\", table \"{tableName}\", column '{columnName}'.{TheStatementHasBeenTerminatedWithSpace}";

        public static string ViolationOfPrimaryKeyConstraint => ViolationOfKeyConstraint("PRIMARY");

        public static string ViolationOfUniqueKeyConstraint => ViolationOfKeyConstraint("UNIQUE");

        private const string TheStatementHasBeenTerminatedWithSpace = "\r\nThe statement has been terminated.";

        private static string ViolationOfKeyConstraint(string keyType) =>
            $"Violation of {keyType} KEY constraint";
    }
}
