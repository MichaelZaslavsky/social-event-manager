using MimeKit;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.Common.Helpers;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

internal sealed class StubEmailSmtpProvider : IEmailProvider
{
    public async Task SendEmailAsync(MimeMessage message)
    {
        await EmailHelpers.SendEmailAsync(new(), message, EmailData.FakePorts);
        BackgroundJobStorage.Instance.Data[BackgroundJobType.Email] = true;
    }
}
