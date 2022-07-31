using System.Net;
using FluentAssertions;
using netDumbster.smtp;
using SocialEventManager.BLL.Models.ContactUs;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.Helpers;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests;

[Collection(TestConstants.StorageDependent)]
[IntegrationTest]
[Category(CategoryConstants.Contacts)]
public class ContactUsControllerTests : IntegrationTest
{
    public ContactUsControllerTests(ApiWebApplicationFactory fixture)
      : base(fixture)
    {
    }

    [Theory]
    [MemberData(nameof(ContactUsData.ValidContactUs), MemberType = typeof(ContactUsData))]
    public async Task Post_Should_ReturnOk_When_ContactDetailsIsValid(ContactUsDto contactUs)
    {
        SimpleSmtpServer smtp = SimpleSmtpServer.Start(EmailData.FakePort);

        await Client.CreateAsync(ApiPathConstants.ContactUs, contactUs);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        SmtpMessage[] emails = smtp.ReceivedEmail;
        emails.Should().HaveCount(1);

        SmtpMessage actual = emails[0];
        actual.Subject.Should().Be(MessageConstants.ContactUsInfo(contactUs.Name, contactUs.Email));
        actual.MessageParts.Select(mp => mp.BodyData).First().Should().Be(contactUs.Text);

        smtp.Stop();
    }

    [Theory]
    [MemberData(nameof(ContactUsData.InvalidContactUs), MemberType = typeof(ContactUsData))]
    public async Task Post_Should_ReturnBadRequest_When_ContactDetailsIsInvalid(ContactUsDto contactUs, string expected)
    {
        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(ApiPathConstants.ContactUs, contactUs);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
        message.Should().Contain(expected);
    }
}
