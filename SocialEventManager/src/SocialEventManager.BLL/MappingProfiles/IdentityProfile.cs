using AutoMapper;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.BLL.MappingConfigurations;

public sealed class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<UserRegistrationDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}
