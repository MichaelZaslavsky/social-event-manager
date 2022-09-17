using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Contact;

namespace SocialEventManager.API.Validations.Contact;

public sealed class ContactDtoValidator : AbstractValidator<ContactDto>
{
    public ContactDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .Length(LengthConstants.Length2, LengthConstants.Length255);

        RuleFor(c => c.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(c => c.Text)
            .NotNull()
            .Length(LengthConstants.Length2, LengthConstants.LengthMax);
    }
}
