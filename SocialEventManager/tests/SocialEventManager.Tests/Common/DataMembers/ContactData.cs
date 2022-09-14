using Bogus;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Models.Contact;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

internal static class ContactData
{
    public static TheoryData<ContactDto> ValidContact =>
        new()
        {
            {
                GetContact()
            },
        };

    public static TheoryData<ContactDto, string> InvalidContact =>
        new()
        {
            {
                GetContact(name: "1"),
                ValidationConstants.LengthNotInRange(nameof(ContactDto.Name), LengthConstants.Length2, LengthConstants.Length255)
            },
            {
                GetContact(name: new Faker().Random.String(LengthConstants.Length255 + 1)),
                ValidationConstants.LengthNotInRange(nameof(ContactDto.Name), LengthConstants.Length2, LengthConstants.Length255)
            },
            {
                GetContact(text: "1"),
                ValidationConstants.LengthNotInRange(nameof(ContactDto.Text), LengthConstants.Length2, LengthConstants.LengthMax)
            },
            {
                GetContact(text: new Faker().Random.String(LengthConstants.LengthMax + 1)),
                ValidationConstants.LengthNotInRange(nameof(ContactDto.Text), LengthConstants.Length2, LengthConstants.LengthMax)
            },
            {
                GetContact(email: TestConstants.SomeText),
                ValidationConstants.InvalidEmail(nameof(ContactDto.Email))
            },
            {
                GetContact(name: null!),
                ValidationConstants.TheFieldIsRequired(nameof(ContactDto.Name))
            },
            {
                GetContact(email: null!),
                ValidationConstants.TheFieldIsRequired(nameof(ContactDto.Email))
            },
            {
                GetContact(text: null!),
                ValidationConstants.TheFieldIsRequired(nameof(ContactDto.Text))
            },
        };

    private static ContactDto GetContact(string name = TestConstants.SomeText, string email = TestConstants.ValidEmail, string text = TestConstants.LoremIpsum) =>
        new(name, email, text);
}
