using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.Common.Helpers;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

internal class StubSmtpClient : SmtpClient, ISmtpClient
{
    public new Task AuthenticateAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public override Task ConnectAsync(string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public override Task DisconnectAsync(bool quit, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public override async Task<string> SendAsync(MimeMessage message, CancellationToken cancellationToken = default, ITransferProgress? progress = null)
    {
        await EmailHelpers.SendEmailAsync(new(), message, EmailData.FakePorts);
        BackgroundJobStorage.Instance.Data[BackgroundJobType.Email] = true;

        return "Success";
    }
}
