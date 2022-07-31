using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Models;

namespace SocialEventManager.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;
    private readonly IEmailProvider _emailProvider;

    public EmailService(IOptions<EmailConfiguration> emailConfiguration, IEmailProvider emailProvider)
    {
        _emailConfiguration = emailConfiguration.Value;
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

        email.From.Add(MailboxAddress.Parse(_emailConfiguration.UserName));
        email.To.AddRange(request.To.Select(to => MailboxAddress.Parse(to)));
        email.Cc.AddRange(request.Cc.Select(cc => MailboxAddress.Parse(cc)));
        email.Bcc.AddRange(request.Bcc.Select(bcc => MailboxAddress.Parse(bcc)));

        return email;
    }
}
