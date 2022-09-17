using System.Diagnostics.CodeAnalysis;

namespace SocialEventManager.Shared.Constants.Validations;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public static class UserValidationConstants
{
    public static string CouldNotInsertUser(string email) =>
        $"Could not insert user {email}.";

    public static string CouldNotUpdateUser(string email) =>
        $"Could not update user {email}.";

    public static string CouldNotDeleteUser(string email) =>
        $"Could not delete user {email}.";
}
