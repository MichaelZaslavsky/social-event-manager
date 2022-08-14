using SocialEventManager.Shared.Models.Contact;
using SocialEventManager.Shared.Constants.Validations;
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
                ValidationConstants.LessThanMinimumLength(nameof(ContactDto.Name), 2)
            },
            {
                GetContact(text: "123456789"),
                ValidationConstants.LessThanMinimumLength(nameof(ContactDto.Text), 10)
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
