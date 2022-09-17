using System.Diagnostics.CodeAnalysis;

namespace SocialEventManager.Shared.Constants.Validations;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public static class RoleValidationConstants
{
    public static string CouldNotInsertRole(string name) =>
        $"Could not insert role {name}.";

    public static string CouldNotUpdateRole(string name) =>
        $"Could not update role {name}.";

    public static string CouldNotDeleteRole(string name) =>
        $"Could not delete role {name}.";
}
