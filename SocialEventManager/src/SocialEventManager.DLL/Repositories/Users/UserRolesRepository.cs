using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SocialEventManager.DLL.Constants;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers.Queries;

namespace SocialEventManager.DLL.Repositories.Users
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
            string cmd = $@"
                INSERT  INTO {TableNameConstants.UserRoles} (UserId, RoleId)
                SELECT  @UserId,
                        R.Id
                FROM    {TableNameConstants.Roles} R
                WHERE   R.NormalizedName = @RoleName;

                {QueryConstants.SelectIdentity}";

            return await _session.Connection.ExecuteAsync(cmd, new DynamicParameters(new { userId, roleName }), _session.Transaction);
        }

        public async Task<IEnumerable<UserRole>> GetUserRoles(string roleName)
        {
            string cmd = $@"
                SELECT  UR.*
                FROM    {TableNameConstants.UserRoles} UR
                WHERE   {RoleQueryHelpers.ExistsByRoleName()};";

            return await _session.Connection.QueryAsync<UserRole>(cmd, new DynamicParameters(new { roleName }), _session.Transaction);
        }

        public async Task<bool> DeleteUserRole(Guid userId, string roleName)
        {
            string cmd = $@"
                DELETE  UR
                FROM    {TableNameConstants.UserRoles} UR
                WHERE   {RoleQueryHelpers.ExistsByRoleName()};

                {QueryConstants.SelectRowCount}";

            return await _session.Connection.ExecuteAsync(cmd, new DynamicParameters(new { userId, roleName }), _session.Transaction) > 0;
        }

        public async Task<bool> IsInRole(Guid userId, string roleName)
        {
            string cmd = $@"
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

            return await _session.Connection.QuerySingleAsync<bool>(cmd, new DynamicParameters(new { userId, roleName }), _session.Transaction);
        }
    }
}
