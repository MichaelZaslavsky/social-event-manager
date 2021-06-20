using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DLL.Repositories.Roles
{
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
            string cmd = $@"
                INSERT INTO {TableNameConstants.Roles} (Id, ConcurrencyStamp, [Name], NormalizedName)
                OUTPUT Inserted.Id
                VALUES (@Id, @ConcurrencyStamp, @Name, @NormalizedName);";

            return await _session.Connection.ExecuteScalarAsync<Guid>(
                cmd, new DynamicParameters(new { role.Id, role.ConcurrencyStamp, role.Name, role.NormalizedName }), _session.Transaction);
        }

        public async Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId)
        {
            string cmd = $@"
                SELECT  R.*
                FROM    {TableNameConstants.Roles} R
                WHERE   EXISTS
                        (
                            SELECT  TOP 1 1
                            FROM    {TableNameConstants.UserRoles} UR
                            WHERE   R.Id = UR.RoleId
                                    AND UR.UserId = @UserId
                        );";

            return await _session.Connection.QueryAsync<Role>(cmd, new DynamicParameters(new { userId }), _session.Transaction);
        }
    }
}
