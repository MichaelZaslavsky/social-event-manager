using Mapster;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Contact;
using SocialEventManager.Shared.Models.Email;

namespace SocialEventManager.BLL.MappingConfigs;

public sealed class ContactMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ContactDto, EmailDto>()
            .Map(dest => dest.To, _ => new[] { ContactConstants.Email })
            .Map(dest => dest.Subject, src => MessageConstants.ContactInfo(src.Name, src.Email))
            .Map(dest => dest.Body, src => src.Text);
    }
}
