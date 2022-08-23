using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Auth;

public record ResetPasswordDto
{
    [Required]
    [EmailAddress]
    [StringLength(IdentityConstants.MaxEmailLength, MinimumLength = IdentityConstants.MinEmailLength)]
    public string Email { get; init; } = null!;

    [Required]
    public string Token { get; init; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(IdentityConstants.MaxPasswordLength, MinimumLength = IdentityConstants.MinPasswordLength)]
    public string NewPassword { get; init; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; init; } = null!;
}
