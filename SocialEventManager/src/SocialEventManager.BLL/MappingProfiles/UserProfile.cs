using System;
using AutoMapper;
using SocialEventManager.BLL.Models;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.DLL.Entities;

namespace SocialEventManager.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterUserDto, ApplicationUser>();

            CreateMap<ApplicationUser, Account>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.DateTime : (DateTime?)null));

            CreateMap<Account, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId.ToString()))
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? new DateTimeOffset(src.LockoutEnd.Value) : (DateTimeOffset?)null));
        }
    }
}
