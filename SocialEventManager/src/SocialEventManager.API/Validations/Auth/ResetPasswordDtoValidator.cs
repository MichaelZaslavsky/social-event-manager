using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.Validations.Auth;

public sealed class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(rp => rp.Email)
            .NotNull()
            .Length(IdentityConstants.MinEmailLength, IdentityConstants.MaxEmailLength)
            .EmailAddress();

        RuleFor(rp => rp.Token)
            .NotNull();

        RuleFor(rp => rp.NewPassword)
            .NotNull()
            .Length(IdentityConstants.MinPasswordLength, IdentityConstants.MaxPasswordLength);

        RuleFor(rp => rp.ConfirmPassword)
            .NotNull()
            .Equal(rp => rp.NewPassword);
    }
}
