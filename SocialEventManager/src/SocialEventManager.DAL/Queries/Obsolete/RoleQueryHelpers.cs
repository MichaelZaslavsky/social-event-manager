// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Queries;

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
