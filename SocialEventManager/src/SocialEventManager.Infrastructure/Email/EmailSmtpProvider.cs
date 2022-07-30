using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialEventManager.Shared.Configurations;

namespace SocialEventManager.Infrastructure.Email;

public class EmailSmtpProvider : IEmailProvider
{
    private const int Port = 587;
    private readonly IOptions<EmailConfiguration> _emailConfiguration;

    public EmailSmtpProvider(IOptions<EmailConfiguration> emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public async Task SendEmailAsync(MimeMessage email)
    {
        SmtpClient smtp = new();

        await smtp.ConnectAsync(_emailConfiguration.Value.Host, Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailConfiguration.Value.UserName, _emailConfiguration.Value.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
