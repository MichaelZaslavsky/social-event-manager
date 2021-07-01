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
        public const string ViolationOfPrimaryKeyConstraint = "Violation of PRIMARY KEY constraint";

        public static string CannotInsertDuplicateKey(string tableName, string indexName, string value) =>
            $"Cannot insert duplicate key row in object '{tableName}' with unique index '{indexName}'. The duplicate key value is ({value}).\r\nThe statement has been terminated.";
    }
}
