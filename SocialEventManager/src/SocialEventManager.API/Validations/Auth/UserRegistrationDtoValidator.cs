using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.Validations.Auth;

public sealed class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
{
    public UserRegistrationDtoValidator()
    {
        RuleFor(ur => ur.FirstName)
            .NotNull()
            .MaximumLength(LengthConstants.Length255);

        RuleFor(ur => ur.LastName)
            .NotNull()
            .MaximumLength(LengthConstants.Length255);

        RuleFor(ur => ur.Email)
            .NotNull()
            .Length(IdentityConstants.MinEmailLength, IdentityConstants.MaxEmailLength)
            .EmailAddress();

        RuleFor(ur => ur.Password)
            .NotNull()
            .Length(IdentityConstants.MinPasswordLength, IdentityConstants.MaxPasswordLength);

        RuleFor(ur => ur.ConfirmPassword)
            .NotNull()
            .Equal(ur => ur.Password);
    }
}
