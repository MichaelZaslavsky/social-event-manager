using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using netDumbster.smtp;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Contact;
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
public sealed class ContactControllerTests : IntegrationTest
{
    public ContactControllerTests(ApiWebApplicationFactory fixture, IJwtHandler jwtHandler)
      : base(fixture, jwtHandler)
    {
    }

    [Theory]
    [MemberData(nameof(ContactData.ValidContact), MemberType = typeof(ContactData))]
    public async Task Post_Should_ReturnOk_When_ContactDetailsIsValid(ContactDto contact)
    {
        await Client.CreateAsync(ApiPathConstants.Contact, contact);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        SmtpMessage[] emails = Smtp.ReceivedEmail;
        emails.Should().HaveCount(1);

        SmtpMessage actual = emails[0];
        actual.Subject.Should().Be(MessageConstants.ContactInfo(contact.Name, contact.Email));
        actual.MessageParts.Select(mp => mp.BodyData).First().Should().Be(contact.Text);
    }

    [Theory]
    [MemberData(nameof(ContactData.InvalidContact), MemberType = typeof(ContactData))]
    public async Task Post_Should_ReturnBadRequest_When_ContactDetailsIsInvalid(ContactDto contact, string expected)
    {
        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(ApiPathConstants.Contact, contact);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
        message.Should().Contain(expected);
    }

    [Theory]
    [MemberData(nameof(ContactData.ValidContact), MemberType = typeof(ContactData))]
    public async Task Post_Should_ReturnOk_When_ContactDetailsIsValidAndEmailProviderIsNotFaked(ContactDto contact)
    {
        await Factory.DisposeAsync();

        HttpClient client = Factory
            .WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddScoped<IEmailProvider, EmailSmtpProvider>()))
            .CreateClient();

        await client.CreateAsync(ApiPathConstants.Contact, contact);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        SmtpMessage[] emails = Smtp.ReceivedEmail;
        emails.Should().HaveCount(1);

        SmtpMessage actual = emails[0];
        actual.Subject.Should().Be(MessageConstants.ContactInfo(contact.Name, contact.Email));
        actual.MessageParts.Select(mp => mp.BodyData).First().Should().Be(contact.Text);
    }
}
