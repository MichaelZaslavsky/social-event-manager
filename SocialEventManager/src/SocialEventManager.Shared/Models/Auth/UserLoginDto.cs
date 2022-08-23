using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Auth;

public record UserLoginDto
{
    [Required]
    [EmailAddress]
    [StringLength(IdentityConstants.MaxEmailLength, MinimumLength = IdentityConstants.MinEmailLength)]
    public string Email { get; init; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(IdentityConstants.MaxPasswordLength, MinimumLength = IdentityConstants.MinPasswordLength)]
    public string Password { get; init; } = null!;
}
