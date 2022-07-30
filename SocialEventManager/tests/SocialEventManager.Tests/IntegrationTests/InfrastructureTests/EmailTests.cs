using FluentAssertions;
using MimeKit;
using MimeKit.Text;
using netDumbster.smtp;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.Helpers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests;

/// <summary>
/// Tests email with a fake SMTP.
/// If email visualization is needed, it is possible to use smtp4dev (https://github.com/rnwood/smtp4dev/wiki/Installation) tool.
/// Install it and then run smtp4dev on port 25 before running the email tests.
/// The emails will be displayed in a local mailbox which can be visualized at http://localhost:5000/.
/// </summary>
[IntegrationTest]
[Category(CategoryConstants.Infrastructure)]
public class EmailTests
{
    [Theory]
    [MemberData(nameof(EmailData.EmailMessage), MemberType = typeof(EmailData))]
    public async Task SendAsync_Should_SendsFakeEmail_When_EmailIsValid(MimeMessage message)
    {
        SimpleSmtpServer smtp = SimpleSmtpServer.Start(EmailData.FakePort);

        await EmailHelpers.SendEmailAsync(new(), message, EmailData.FakePorts);

        SmtpMessage[] emails = smtp.ReceivedEmail;
        emails.Should().HaveCount(message.To.Count);

        SmtpMessage actual = emails[0];
        actual.Subject.Should().Be(message.Subject);
        actual.MessageParts.Select(mp => mp.BodyData).First().Should().Be(message.GetTextBody(TextFormat.Html));
    }
}
