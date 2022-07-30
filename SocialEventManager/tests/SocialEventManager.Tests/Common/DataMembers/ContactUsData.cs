using SocialEventManager.BLL.Models.ContactUs;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

internal static class ContactUsData
{
    public static TheoryData<ContactUsDto> ValidContactUs =>
        new()
        {
            {
                GetContactUs()
            },
        };

    public static TheoryData<ContactUsDto, string> InvalidContactUs =>
        new()
        {
            {
                GetContactUs(name: "1"),
                ValidationConstants.LessThanMinimumLength(nameof(ContactUsDto.Name), 2)
            },
            {
                GetContactUs(text: "123456789"),
                ValidationConstants.LessThanMinimumLength(nameof(ContactUsDto.Text), 10)
            },
            {
                GetContactUs(email: TestConstants.SomeText),
                ValidationConstants.InvalidEmail(nameof(ContactUsDto.Email))
            },
            {
                GetContactUs(name: null!),
                ValidationConstants.TheFieldIsRequired(nameof(ContactUsDto.Name))
            },
            {
                GetContactUs(email: null!),
                ValidationConstants.TheFieldIsRequired(nameof(ContactUsDto.Email))
            },
            {
                GetContactUs(text: null!),
                ValidationConstants.TheFieldIsRequired(nameof(ContactUsDto.Text))
            },
        };

    private static ContactUsDto GetContactUs(string name = TestConstants.SomeText, string email = TestConstants.ValidEmail, string text = TestConstants.LoremIpsum) =>
        new(name, email, text);
}
