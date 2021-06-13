using AutoMapper;
using SocialEventManager.BLL.Models;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.DLL.Entities;

namespace SocialEventManager.BLL.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleForCreationDto, Role>();
            CreateMap<RoleForUpdateDto, Role>();

            CreateMap<ApplicationRole, RoleForCreationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ApplicationRole, RoleForUpdateDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<RoleDto, ApplicationRole>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
