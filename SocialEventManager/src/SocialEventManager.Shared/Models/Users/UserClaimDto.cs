using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Shared.Models.Users;

public record UserClaimDto : UserClaimBase
{
    [Required]
    [NotDefault]
    public Guid UserId { get; set; }
}
