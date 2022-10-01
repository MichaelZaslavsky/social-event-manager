using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Mapster;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Users;

namespace SocialEventManager.BLL.MappingConfigs;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterUserDto, ApplicationUser>();
        config.NewConfig<UserClaim, UserClaimDto>();
        config.NewConfig<Claim, UserClaimDto>();
        config.NewConfig<Claim, UserClaimForCreationDto>();
        config.NewConfig<UserClaimForCreationDto, UserClaim>();
        config.NewConfig<UserClaimForUpdateDto, UserClaim>();
    }
}
