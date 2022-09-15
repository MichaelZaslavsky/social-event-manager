using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.Validations.Auth;

public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
{
    public ConfirmEmailDtoValidator()
    {
        RuleFor(ce => ce.Email)
            .NotNull()
            .Length(IdentityConstants.MinEmailLength, IdentityConstants.MaxEmailLength)
            .EmailAddress();

        RuleFor(ce => ce.Token)
            .NotNull();
    }
}
