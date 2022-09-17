using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.Validations.Auth;

public sealed class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordDtoValidator()
    {
        RuleFor(fp => fp.Email)
            .NotNull()
            .Length(IdentityConstants.MinEmailLength, IdentityConstants.MaxEmailLength)
            .EmailAddress();
    }
}
