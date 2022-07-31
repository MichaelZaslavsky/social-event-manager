using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialEventManager.Shared.Configurations;

namespace SocialEventManager.Infrastructure.Email;

public class EmailSmtpProvider : IEmailProvider
{
    private const int Port = 587;
    private readonly EmailConfiguration _emailConfiguration;

    public EmailSmtpProvider(IOptions<EmailConfiguration> emailConfiguration)
    {
        _emailConfiguration = emailConfiguration.Value;
    }

    public async Task SendEmailAsync(MimeMessage email)
    {
        SmtpClient smtp = new();

        await smtp.ConnectAsync(_emailConfiguration.Host, Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
