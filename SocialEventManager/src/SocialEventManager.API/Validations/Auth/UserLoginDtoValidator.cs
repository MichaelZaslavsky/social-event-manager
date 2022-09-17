using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.Validations.Auth;

public sealed class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(ul => ul.Email)
            .NotNull()
            .Length(IdentityConstants.MinEmailLength, IdentityConstants.MaxEmailLength)
            .EmailAddress();

        RuleFor(ul => ul.Password)
            .NotNull()
            .Length(IdentityConstants.MinPasswordLength, IdentityConstants.MaxPasswordLength);
    }
}
