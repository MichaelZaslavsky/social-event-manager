using Bogus;
using MimeKit;
using MimeKit.Text;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

internal static class EmailData
{
    public const int FakePort = 9009;
    private const int LocalhostSmtpPort = 25;

    public static int[] FakePorts => new[] { LocalhostSmtpPort, FakePort };

    public static TheoryData<MimeMessage> EmailMessage =>
        new()
        {
            {
                GetMessage()
            },
        };

    public static TheoryData<string, string?, IEnumerable<string>, IEnumerable<string>> EmailDtoPartialData =>
        new()
        {
            {
                new Faker().Lorem.Paragraph(),
                new Faker().Lorem.Text(),
                new[] { new Faker().Internet.Email(new Faker().Internet.Email()) },
                new[] { new Faker().Internet.Email(new Faker().Internet.Email()) }
            },
        };

    public static TheoryData<string, string?, IEnumerable<string>, IEnumerable<string>, IEnumerable<string>> EmailDtoFullData =>
        new()
        {
            {
                new Faker().Lorem.Paragraph(),
                new Faker().Lorem.Text(),
                new[] { new Faker().Internet.Email(new Faker().Internet.Email()) },
                new[] { new Faker().Internet.Email(new Faker().Internet.Email()) },
                new[] { new Faker().Internet.Email(new Faker().Internet.Email()) }
            },
        };

    private static MimeMessage GetMessage(
        string fromAddress = TestConstants.ValidEmail,
        string toAddress = TestConstants.OtherValidEmail,
        string subject = TestConstants.SomeText,
        string text = TestConstants.LoremIpsum)
    {
        MimeMessage message = new()
        {
            Subject = subject,
            Body = new TextPart(TextFormat.Html)
            {
                Text = text,
            },
        };

        message.From.Add(MailboxAddress.Parse(fromAddress));
        message.To.Add(MailboxAddress.Parse(toAddress));

        return message;
    }
}
