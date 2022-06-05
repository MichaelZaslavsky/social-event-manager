using Dapper;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Repositories.Roles;

public class RolesRepository : GenericRepository<Role>, IRolesRepository
{
    private readonly IDbSession _session;

    public RolesRepository(IDbSession session)
        : base(session)
    {
        _session = session;
    }

    public async Task<Guid> InsertRole(Role role)
    {
        string sql = $@"
                INSERT INTO {TableNameConstants.Roles} (Id, ConcurrencyStamp, [Name], NormalizedName)
                OUTPUT Inserted.Id
                VALUES (@Id, @ConcurrencyStamp, @Name, @NormalizedName);";

        return await _session.Connection.ExecuteScalarAsync<Guid>(
            sql, new DynamicParameters(new { role.Id, role.ConcurrencyStamp, role.Name, role.NormalizedName }), _session.Transaction);
    }

    public async Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId)
    {
        string sql = $@"
                SELECT  R.*
                FROM    {TableNameConstants.Roles} R
                WHERE   EXISTS
                        (
                            SELECT  TOP 1 1
                            FROM    {TableNameConstants.UserRoles} UR
                            WHERE   R.Id = UR.RoleId
                                    AND UR.UserId = @UserId
                        );";

        return await _session.Connection.QueryAsync<Role>(sql, new DynamicParameters(new { userId }), _session.Transaction);
    }
}
