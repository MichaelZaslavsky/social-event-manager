using SocialEventManager.Shared.Models;

namespace SocialEventManager.Infrastructure.Email;

public interface IEmailService
{
    Task SendEmailAsync(EmailDto request);
}
