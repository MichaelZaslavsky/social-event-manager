using MimeKit;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.Helpers;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

internal class StubEmailSmtpProvider : IEmailProvider
{
    public async Task SendEmailAsync(MimeMessage message)
    {
        await EmailHelpers.SendEmailAsync(new(), message, EmailData.FakePorts);
    }
}
