using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Models;

namespace SocialEventManager.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly IOptions<EmailConfiguration> _emailConfiguration;
    private readonly IEmailProvider _emailProvider;

    public EmailService(IOptions<EmailConfiguration> emailConfiguration, IEmailProvider emailProvider)
    {
        _emailConfiguration = emailConfiguration;
        _emailProvider = emailProvider;
    }

    public async Task SendEmailAsync(EmailDto request)
    {
        MimeMessage email = BuildEmail(request);
        await _emailProvider.SendEmailAsync(email);
    }

    private MimeMessage BuildEmail(EmailDto request)
    {
        MimeMessage email = new()
        {
            Subject = request.Subject,
            Body = request.Body is null ? null : new TextPart(TextFormat.Html) { Text = request.Body },
        };

        email.From.Add(MailboxAddress.Parse(_emailConfiguration.Value.UserName));

        foreach (string to in request.To)
        {
            email.To.Add(MailboxAddress.Parse(to));
        }

        foreach (string cc in request.Cc)
        {
            email.Cc.Add(MailboxAddress.Parse(cc));
        }

        foreach (string bcc in request.Bcc)
        {
            email.To.Add(MailboxAddress.Parse(bcc));
        }

        return email;
    }
}
