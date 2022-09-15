using Microsoft.AspNetCore.Identity;

namespace SocialEventManager.Shared.Models.Auth;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}
