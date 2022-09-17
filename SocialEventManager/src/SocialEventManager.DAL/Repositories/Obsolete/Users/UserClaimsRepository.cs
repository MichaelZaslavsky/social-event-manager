// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using Dapper;
using Newtonsoft.Json;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Users;

public sealed class UserClaimsRepository : GenericRepository<UserClaim>, IUserClaimsRepository
{
    private readonly IDbSession _session;

    public UserClaimsRepository(IDbSession session)
        : base(session)
    {
        _session = session;
    }

    public async Task<IEnumerable<UserClaim>> GetUserClaims(string type, string value)
    {
        string sql = $@"
            SELECT  UC.*
            FROM    {TableNameConstants.UserClaims} UC
            WHERE   UC.[{nameof(UserClaim.Type)}] = @Type
                    AND UC.[{nameof(UserClaim.Value)}] = @Value;";

        return await _session.Connection.QueryAsync<UserClaim>(sql, new DynamicParameters(new { type, value }), _session.Transaction);
    }

    public async Task<bool> DeleteUserClaims(IEnumerable<UserClaim> userClaims)
    {
        string userClaimsJson = JsonConvert.SerializeObject(userClaims);

        string sql = $@"
            DELETE  UC
            FROM    {TableNameConstants.UserClaims} UC
                    INNER JOIN OPENJSON(@UserClaimsJson)
                    WITH
                    (
                        UserId  UNIQUEIDENTIFIER    N'$.UserId',
                        [Type]  NVARCHAR(255)       N'$.Type',
                        [Value] NVARCHAR(255)       N'$.Value'
                    ) AS UCJ ON UC.UserId = UCJ.UserId
                        AND UC.[{nameof(UserClaim.Type)}] = UCJ.[Type]
                        AND UC.[{nameof(UserClaim.Value)}] = UCJ.[Value];";

        return await _session.Connection.ExecuteAsync(sql, new DynamicParameters(new { userClaimsJson }), _session.Transaction) > 0;
    }
}
*/
