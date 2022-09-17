using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Roles;

namespace SocialEventManager.BLL.MappingProfiles;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class RoleProfile : Profile
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
