using AutoMapper;
using SocialEventManager.BLL.Models.ContactUs;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models;

namespace SocialEventManager.BLL.MappingProfiles;

public class ContactUsProfile : Profile
{
    public ContactUsProfile()
    {
        CreateMap<ContactUsDto, EmailDto>()
            .ForMember(dest => dest.To, opt => opt.MapFrom(_ => new[] { ContactConstants.Email }))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => MessageConstants.ContactUsInfo(src.Name, src.Email)))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Text));
    }
}
