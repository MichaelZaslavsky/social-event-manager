using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Helpers.Queries
{
    public static class RoleQueryHelpers
    {
        public static string ExistsByRoleName(string userRolesAlias = "UR")
        {
            return $@"
                EXISTS
                (
                    SELECT  TOP 1 1
                    FROM    {TableNameConstants.Roles} R
                    WHERE   {userRolesAlias}.RoleId = R.Id
                            AND R.NormalizedName = @RoleName
                )";
        }
    }
}
