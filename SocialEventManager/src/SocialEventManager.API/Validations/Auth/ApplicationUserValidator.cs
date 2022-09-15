using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.Validations.Auth;

public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
{
    public ApplicationUserValidator()
    {
        RuleFor(au => au.FirstName)
            .NotNull()
            .MaximumLength(LengthConstants.Length255);

        RuleFor(au => au.LastName)
            .NotNull()
            .MaximumLength(LengthConstants.Length255);
    }
}
