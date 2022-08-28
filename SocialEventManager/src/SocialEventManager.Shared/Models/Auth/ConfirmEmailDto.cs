using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Auth;

public record ConfirmEmailDto
{
    [Required]
    [EmailAddress]
    [StringLength(IdentityConstants.MaxEmailLength, MinimumLength = IdentityConstants.MinEmailLength)]
    public string Email { get; init; } = null!;

    [Required]
    public string Token { get; set; } = null!;
}
