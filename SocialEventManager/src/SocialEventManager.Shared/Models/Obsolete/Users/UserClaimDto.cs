using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Shared.Models.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public record UserClaimDto : UserClaimBase
{
    [Required]
    [NotDefault]
    public Guid UserId { get; set; }
}
