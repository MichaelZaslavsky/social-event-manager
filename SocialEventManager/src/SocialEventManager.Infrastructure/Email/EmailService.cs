using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Extensions;
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
        request.To.ForEach(to => email.To.Add(MailboxAddress.Parse(to)));
        request.Cc.ForEach(cc => email.Cc.Add(MailboxAddress.Parse(cc)));
        request.Bcc.ForEach(bcc => email.Bcc.Add(MailboxAddress.Parse(bcc)));

        return email;
    }
}
