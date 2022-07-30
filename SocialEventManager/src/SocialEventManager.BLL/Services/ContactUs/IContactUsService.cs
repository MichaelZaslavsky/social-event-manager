using SocialEventManager.BLL.Models.ContactUs;

namespace SocialEventManager.BLL.Services.ContactUs;

public interface IContactUsService
{
    Task ContactUsAsync(ContactUsDto contactUs);
}
