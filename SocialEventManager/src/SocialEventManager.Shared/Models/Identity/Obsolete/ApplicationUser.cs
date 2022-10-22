using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Identity;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class ApplicationUser : IdentityUser
{
    required public string AuthenticationType { get; set; }

    public bool IsAuthenticated { get; set; }

    required public string Name { get; set; }
}
