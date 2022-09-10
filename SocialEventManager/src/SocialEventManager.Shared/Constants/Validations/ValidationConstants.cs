namespace SocialEventManager.Shared.Constants.Validations;

public static class ValidationConstants
{
    public const string ObjectIsNull = "Object is null";
    public const string WasNotFound = "was not found.";
    public const string WereNotFound = "were not found.";
    public const string NotAValidIdentifier = "Not a valid identifier.";
    public const string TheFieldMustNotHaveTheDefaultValue = "The {0} field must not have the default value.";

    public static string InvalidEmail(string fieldName) => $"The {fieldName} field is not a valid e-mail address.";

    public static string TheFieldIsRequired(string fieldName) => $"The {fieldName} field is required.";

    public static string LengthNotInRange(string filedName, int minimum, int maximum) =>
        $"The field {filedName} must be a string with a minimum length of {minimum} and a maximum length of {maximum}.";

    public static string CouldNotFindThisView(string viewPath, string searchedLocations) =>
        $"Could not find this view: {viewPath}. Searched locations:\n{searchedLocations}";
}
