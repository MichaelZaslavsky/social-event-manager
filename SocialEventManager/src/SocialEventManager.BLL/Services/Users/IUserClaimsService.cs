using SocialEventManager.BLL.Models.Users;

namespace SocialEventManager.BLL.Services.Users
{
    public interface IUserClaimsService
    {
        Task CreateUserClaims(IEnumerable<UserClaimForCreationDto> userClaimsForCreation);

        Task<IEnumerable<UserClaimDto>> GetUserClaims(Guid userId);

        Task<IEnumerable<UserClaimDto>> GetUserClaims(string type, string value);

        Task<bool> ReplaceUserClaim(UserClaimBase currentUserClaim, UserClaimForUpdateDto newUserClaimForUpdate);

        Task<bool> DeleteUserClaims(IEnumerable<UserClaimBase> userClaimsBase, Guid userId);
    }
}
