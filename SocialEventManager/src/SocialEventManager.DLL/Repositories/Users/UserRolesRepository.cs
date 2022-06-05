using Dapper;
using SocialEventManager.DAL.Constants;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers.Queries;

namespace SocialEventManager.DAL.Repositories.Users
{
    public class UserRolesRepository : GenericRepository<UserRole>, IUserRolesRepository
    {
        private readonly IDbSession _session;

        public UserRolesRepository(IDbSession session)
            : base(session)
        {
            _session = session;
        }

        public async Task<int> InsertAsync(Guid userId, string roleName)
        {
            string sql = $@"
                INSERT  INTO {TableNameConstants.UserRoles} (UserId, RoleId)
                SELECT  @UserId,
                        R.Id
                FROM    {TableNameConstants.Roles} R
                WHERE   R.NormalizedName = @RoleName;

                {QueryConstants.SelectScopeIdentity}";

            return await _session.Connection.ExecuteAsync(sql, new DynamicParameters(new { userId, roleName }), _session.Transaction);
        }

        public async Task<IEnumerable<UserRole>> GetUserRoles(string roleName)
        {
            string sql = $@"
                SELECT  UR.*
                FROM    {TableNameConstants.UserRoles} UR
                WHERE   {RoleQueryHelpers.ExistsByRoleName()};";

            return await _session.Connection.QueryAsync<UserRole>(sql, new DynamicParameters(new { roleName }), _session.Transaction);
        }

        public async Task<bool> DeleteUserRole(Guid userId, string roleName)
        {
            string sql = $@"
                DELETE  UR
                FROM    {TableNameConstants.UserRoles} UR
                WHERE   UR.UserId = @UserId
                        AND {RoleQueryHelpers.ExistsByRoleName()};";

            return await _session.Connection.ExecuteAsync(sql, new DynamicParameters(new { userId, roleName }), _session.Transaction) > 0;
        }

        public async Task<bool> IsInRole(Guid userId, string roleName)
        {
            string sql = $@"
                IF EXISTS
                (
                    SELECT  TOP 1 1
                    FROM    {TableNameConstants.UserRoles} UR
                            INNER JOIN {TableNameConstants.Roles} R ON UR.RoleId = R.Id
                    WHERE   UR.UserId = @UserId
                            AND R.NormalizedName = @RoleName
                )
                BEGIN
                    SELECT 1;
                END
                ELSE
                BEGIN
                    SELECT 0;
                END";

            return await _session.Connection.QuerySingleAsync<bool>(sql, new DynamicParameters(new { userId, roleName }), _session.Transaction);
        }
    }
}
