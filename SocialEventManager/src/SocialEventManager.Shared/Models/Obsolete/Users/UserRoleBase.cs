using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Shared.Models.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public record UserRoleBase
{
    public UserRoleBase(string userId, string roleName)
    {
        UserId = Guid.Parse(userId);
        RoleName = roleName;
    }

    public UserRoleBase(Guid userId, string roleName)
    {
        UserId = userId;
        RoleName = roleName;
    }

    [Required]
    [NotDefault]
    public Guid UserId { get; init; }

    [Required(AllowEmptyStrings = false)]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string RoleName { get; init; }
}
