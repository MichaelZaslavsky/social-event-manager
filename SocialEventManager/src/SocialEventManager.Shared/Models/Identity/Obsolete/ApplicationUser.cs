using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Identity;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class ApplicationUser : IdentityUser
{
    public string AuthenticationType { get; set; } = null!;

    public bool IsAuthenticated { get; set; }

    public string Name { get; set; } = null!;
}
