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
        }
    }
}
