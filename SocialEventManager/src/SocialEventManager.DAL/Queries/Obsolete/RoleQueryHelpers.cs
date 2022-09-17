using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Queries;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public static class RoleQueryHelpers
{
    public static string ExistsByRoleName(string userRolesAlias = "UR")
    {
        return $@"
            EXISTS
            (
                SELECT  TOP 1 1
                FROM    {TableNameConstants.Roles} R
                WHERE   {userRolesAlias}.RoleId = R.{nameof(Role.Id)}
                        AND R.{nameof(Role.NormalizedName)} = @RoleName
            )";
    }
}
