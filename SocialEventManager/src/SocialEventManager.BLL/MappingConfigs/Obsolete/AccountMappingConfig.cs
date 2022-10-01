using System.Diagnostics.CodeAnalysis;
using Mapster;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Models.Accounts;
using SocialEventManager.Shared.Models.Identity;

namespace SocialEventManager.BLL.MappingConfigs;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class AccountMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Account, AccountDto>()
            .TwoWays();

        config.NewConfig<AccountForCreationDto, Account>();
        config.NewConfig<AccountForUpdateDto, Account>();

        config.NewConfig<ApplicationUser, AccountForCreationDto>()
            .Map(dest => dest.UserId, src => Guid.Parse(src.Id))
            .Map(dest => dest.LockoutEnd, src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.DateTime : (DateTime?)null);

        config.NewConfig<ApplicationUser, AccountForUpdateDto>()
            .Map(dest => dest.UserId, src => Guid.Parse(src.Id))
            .Map(dest => dest.LockoutEnd, src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.DateTime : (DateTime?)null);

        config.NewConfig<AccountDto, ApplicationUser>()
            .Map(dest => dest.Id, src => src.UserId.ToString())
            .Map(dest => dest.LockoutEnd, src => src.LockoutEnd.HasValue ? new DateTimeOffset(src.LockoutEnd.Value) : (DateTimeOffset?)null);
    }
}
