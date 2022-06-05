using System.Security.Claims;
using AutoMapper;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.BLL.Models.Users;
using SocialEventManager.DAL.Entities;

namespace SocialEventManager.BLL.MappingProfiles;

public class UserProfile : Profile
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
