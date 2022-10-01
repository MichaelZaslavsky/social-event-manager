using Mapster;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.BLL.MappingConfigs;

public sealed class IdentityMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserRegistrationDto, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);
    }
}
