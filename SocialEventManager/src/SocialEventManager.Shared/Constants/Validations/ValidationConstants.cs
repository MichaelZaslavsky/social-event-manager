namespace SocialEventManager.Shared.Constants.Validations;

public static class ValidationConstants
{
    public const string ObjectIsNull = "Object is null";
    public const string WasNotFound = "was not found.";
    public const string WereNotFound = "were not found.";
    public const string NotAValidIdentifier = "Not a valid identifier.";
    public const string TheFieldMustNotHaveTheDefaultValue = "The {0} field must not have the default value.";

    public static string LessThanMinimumLength(string fieldName, int minLength) =>
        $"The field {fieldName} must be a string or array type with a minimum length of '{minLength}'.";

    public static string InvalidEmail(string fieldName) => $"The {fieldName} field is not a valid e-mail address.";

    public static string TheFieldIsRequired(string fieldName) => $"The {fieldName} field is required.";

    public static string FieldMaximumLength(string fieldName, int length) => FieldLength(fieldName, length, true);

    public static string FieldMinimumLength(string fieldName, int length) => FieldLength(fieldName, length, false);

    public static string CouldNotFindThisView(string viewPath, string searchedLocations) =>
        $"Could not find this view: {viewPath}. Searched locations:\n{searchedLocations}";

    private static string FieldLength(string fieldName, int length, bool isMaximum)
    {
        string maxOrMin = isMaximum ? "maximum" : "minimum";
        return $"The field {fieldName} must be a string or array type with a {maxOrMin} length of '{length}'.";
    }
}
