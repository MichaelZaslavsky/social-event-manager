using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Auth;

public record UserRegistrationDto
{
    [Required]
    [StringLength(LengthConstants.Length255)]
    public string FirstName { get; init; } = null!;

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string LastName { get; init; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(IdentityConstants.MaxEmailLength, MinimumLength = IdentityConstants.MinEmailLength)]
    public string Email { get; init; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(IdentityConstants.MaxPasswordLength, MinimumLength = IdentityConstants.MinPasswordLength)]
    public string Password { get; init; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; init; } = null!;
}
