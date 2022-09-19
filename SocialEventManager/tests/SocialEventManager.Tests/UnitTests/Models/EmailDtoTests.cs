using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Email;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.Models;

[UnitTest]
[Category(nameof(UnitTests))]
public class EmailDtoTests
{
    [Theory]
    [MemberData(nameof(EmailData.EmailDtoPartialData), MemberType = typeof(EmailData))]
    public void InitPartialFields_Should_ReturnEmailDto_When_DataIsValid(
        string subject, string? body, IEnumerable<string> to, IEnumerable<string> bcc)
    {
        EmailDto email = new(subject, body, to, bcc);
        AssertEmail(email, subject, body, to, Enumerable.Empty<string>(), bcc);
    }

    [Theory]
    [MemberData(nameof(EmailData.EmailDtoFullData), MemberType = typeof(EmailData))]
    public void InitAllFields_Should_ReturnEmailDto_When_DataIsValid(
        string subject, string? body, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc)
    {
        EmailDto email = new(subject, body, to, cc, bcc);
        AssertEmail(email, subject, body, to, cc, bcc);
    }

    [Theory]
    [AutoData]
    public void InitFields_Should_ThrowInvalidOperationException_When_NoRecipientsHaveBeenSpecified(
        string subject, string? body)
    {
        Action action = () => new EmailDto(subject, body, Enumerable.Empty<string>(), Enumerable.Empty<string>(), Enumerable.Empty<string>());
        action.Should().Throw<InvalidOperationException>().WithMessage(ExceptionConstants.NoRecipientsHaveBeenSpecified);
    }

    private static void AssertEmail(
        EmailDto actualEmail,
        string expectedSubject,
        string? expectedBody,
        IEnumerable<string> expectedTo,
        IEnumerable<string> expectedCc,
        IEnumerable<string> expectedBcc)
    {
        actualEmail.Subject.Should().Be(expectedSubject);
        actualEmail.Body.Should().Be(expectedBody);
        actualEmail.To.Should().BeEquivalentTo(expectedTo);
        actualEmail.Cc.Should().BeEquivalentTo(expectedCc);
        actualEmail.Bcc.Should().BeEquivalentTo(expectedBcc);
    }
}
