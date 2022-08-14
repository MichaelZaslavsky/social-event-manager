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
                WHERE   {userRolesAlias}.{nameof(UserRole.RoleId)} = R.{nameof(Role.Id)}
                        AND R.{nameof(Role.NormalizedName)} = @RoleName
            )";
    }
}
