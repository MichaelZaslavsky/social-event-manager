using SocialEventManager.BLL.Models.Contact;

namespace SocialEventManager.BLL.Services.Contact;

public interface IContactService
{
    Task ContactAsync(ContactDto contact);
}
