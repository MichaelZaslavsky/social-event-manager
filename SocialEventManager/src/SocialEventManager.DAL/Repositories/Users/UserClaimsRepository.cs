using Dapper;
using Newtonsoft.Json;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.DAL.Repositories.Users;

public class UserClaimsRepository : GenericRepository<UserClaim>, IUserClaimsRepository
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
            WHERE   UC.[Type] = @Type
                    AND UC.[Value] = @Value;";

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
                        UserId	UNIQUEIDENTIFIER    N'$.UserId',
                        [Type]  NVARCHAR(255)       N'$.Type',
                        [Value] NVARCHAR(255)       N'$.Value'
                    ) AS UCJ ON UC.UserId = UCJ.UserId
                        AND UC.[Type] = UCJ.[Type]
                        AND UC.[Value] = UCJ.[Value];";

        return await _session.Connection.ExecuteAsync(sql, new DynamicParameters(new { userClaimsJson }), _session.Transaction) > 0;
    }
}
