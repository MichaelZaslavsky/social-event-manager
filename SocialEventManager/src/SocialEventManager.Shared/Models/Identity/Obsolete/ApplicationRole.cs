using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Identity;

/// <summary>
/// The role.
/// </summary>
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName)
        : base(roleName)
    {
    }
}
