using AutoMapper;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Roles;

namespace SocialEventManager.BLL.MappingProfiles;

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
