// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using SocialEventManager.Shared.Models.Users;

namespace SocialEventManager.BLL.Services.Users;

public interface IUserClaimsService
{
    Task CreateUserClaims(IEnumerable<UserClaimForCreationDto> userClaimsForCreation);

    Task<IEnumerable<UserClaimDto>> GetUserClaims(Guid userId);

    Task<IEnumerable<UserClaimDto>> GetUserClaims(string type, string value);

    Task<bool> ReplaceUserClaim(UserClaimBase currentUserClaim, UserClaimForUpdateDto newUserClaimForUpdate);

    Task<bool> DeleteUserClaims(IEnumerable<UserClaimBase> userClaimsBase, Guid userId);
}
*/
