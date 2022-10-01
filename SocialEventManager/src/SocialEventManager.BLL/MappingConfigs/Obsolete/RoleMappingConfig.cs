using System.Diagnostics.CodeAnalysis;
using Mapster;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Roles;

namespace SocialEventManager.BLL.MappingConfigs;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class RoleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, RoleDto>()
            .TwoWays();

        config.NewConfig<RoleForCreationDto, Role>();
        config.NewConfig<RoleForUpdateDto, Role>();

        config.NewConfig<ApplicationRole, RoleForCreationDto>()
            .Map(dest => dest.Id, src => src.Id);

        config.NewConfig<ApplicationRole, RoleForUpdateDto>()
            .Map(dest => dest.Id, src => src.Id);

        config.NewConfig<RoleDto, ApplicationRole>()
            .Map(dest => dest.Id, src => src.Id);
    }
}
