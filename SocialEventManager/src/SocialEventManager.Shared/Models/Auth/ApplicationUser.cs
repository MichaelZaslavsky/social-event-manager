using Microsoft.AspNetCore.Identity;

namespace SocialEventManager.Shared.Models.Auth;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    required public string FirstName { get; init; }

    required public string LastName { get; init; }
}
