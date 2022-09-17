using System.Diagnostics.CodeAnalysis;
using Dapper;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.DAL.Queries;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class UserRolesRepository : GenericRepository<UserRole>, IUserRolesRepository
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
            INSERT  INTO {TableNameConstants.UserRoles} ({nameof(UserRole.UserId)}, {nameof(UserRole.RoleId)})
            SELECT  @UserId,
                    R.{nameof(Role.Id)}
            FROM    {TableNameConstants.Roles} R
            WHERE   R.{nameof(Role.NormalizedName)} = @RoleName;

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
            WHERE   UR.{nameof(UserRole.UserId)} = @UserId
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
                        INNER JOIN {TableNameConstants.Roles} R ON UR.{nameof(UserRole.RoleId)} = R.Id
                WHERE   UR.{nameof(UserRole.UserId)} = @UserId
                        AND R.{nameof(Role.NormalizedName)} = @RoleName
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
