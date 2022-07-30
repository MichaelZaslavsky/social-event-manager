using MimeKit;

namespace SocialEventManager.Infrastructure.Email;

public interface IEmailProvider
{
    Task SendEmailAsync(MimeMessage email);
}
