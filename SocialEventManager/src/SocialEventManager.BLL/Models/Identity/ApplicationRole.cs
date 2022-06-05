using Microsoft.AspNetCore.Identity;

namespace SocialEventManager.BLL.Models.Identity;

/// <summary>
/// The role.
/// </summary>
public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName)
        : base(roleName)
    {
    }
}
