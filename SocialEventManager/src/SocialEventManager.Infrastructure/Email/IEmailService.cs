using SocialEventManager.Shared.Models.Email;

namespace SocialEventManager.Infrastructure.Email;

public interface IEmailService
{
    Task SendEmailAsync(EmailDto request);
}
