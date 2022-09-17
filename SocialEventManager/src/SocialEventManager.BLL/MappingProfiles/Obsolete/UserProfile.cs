using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using AutoMapper;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Users;

namespace SocialEventManager.BLL.MappingProfiles;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, ApplicationUser>();
        CreateMap<UserClaim, UserClaimDto>();
        CreateMap<Claim, UserClaimDto>();
        CreateMap<Claim, UserClaimForCreationDto>();
        CreateMap<UserClaimForCreationDto, UserClaim>();
        CreateMap<UserClaimForUpdateDto, UserClaim>();
    }
}
