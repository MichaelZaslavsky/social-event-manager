namespace SocialEventManager.Tests.Common.Constants
{
    public static class ExceptionConstants
    {
        public const string ValueCannotBeNull = "Value cannot be null. (Parameter 'source')";
        public const string AnErrorOccurred = "An error occurred";
        public const string ExceptionWasA = "Exception was a ";
        public const string NullReferenceException = ExceptionWasA + "null reference exception.";
        public const string TimeoutException = ExceptionWasA + "timeout exception.";
        public const string SqlException = ExceptionWasA + "database exception.";
        public const string Exception = ExceptionWasA + "generic exception.";
        public const string CannotOpenDatabase = "cannot open database";
        public const string ANetworkRelated = "a network-related";
        public const string NotFound = "Not Found";
        public const string BadRequest = "Bad Request";
        public const string InternalServerError = "Internal Server Error";
        public const string DatabaseMigrationFailed = "Database migration failed.";
    }
}
