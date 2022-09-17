using AutoMapper;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Contact;
using SocialEventManager.Shared.Models.Email;

namespace SocialEventManager.BLL.MappingProfiles;

public sealed class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<ContactDto, EmailDto>()
            .ForMember(dest => dest.To, opt => opt.MapFrom(_ => new[] { ContactConstants.Email }))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => MessageConstants.ContactInfo(src.Name, src.Email)))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Text));
    }
}
