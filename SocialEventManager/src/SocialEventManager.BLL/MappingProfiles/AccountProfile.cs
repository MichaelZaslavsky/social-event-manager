using AutoMapper;
using SocialEventManager.Shared.Models.Accounts;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Entities;

namespace SocialEventManager.BLL.MappingProfiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<AccountForCreationDto, Account>();
        CreateMap<AccountForUpdateDto, Account>();

        CreateMap<ApplicationUser, AccountForCreationDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.DateTime : (DateTime?)null));

        CreateMap<ApplicationUser, AccountForUpdateDto>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
           .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.DateTime : (DateTime?)null));

        CreateMap<AccountDto, ApplicationUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? new DateTimeOffset(src.LockoutEnd.Value) : (DateTimeOffset?)null));
    }
}
