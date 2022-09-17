using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialEventManager.Shared.Configurations;

namespace SocialEventManager.Infrastructure.Email;

public sealed class EmailSmtpProvider : IEmailProvider
{
    private const int Port = 587;
    private readonly EmailConfiguration _emailConfiguration;
    private readonly ISmtpClient _smtp;

    public EmailSmtpProvider(IOptions<EmailConfiguration> emailConfiguration, ISmtpClient smtp)
    {
        _emailConfiguration = emailConfiguration.Value;
        _smtp = smtp;
    }

    public async Task SendEmailAsync(MimeMessage email)
    {
        await _smtp.ConnectAsync(_emailConfiguration.Host, Port, SecureSocketOptions.StartTls);
        await _smtp.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
        await _smtp.SendAsync(email);
        await _smtp.DisconnectAsync(true);
    }
}
