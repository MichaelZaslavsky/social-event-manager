using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

/// <summary>
/// The user data for registration.
/// </summary>
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public record RegisterUserDto
{
    /// <summary>
    /// Gets the name of the registered user.
    /// </summary>
    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string UserName { get; init; } = null!;

    /// <summary>
    /// Gets the password of the registered user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = null!;

    /// <summary>
    /// Gets the confirmation password of the registered user.
    /// It should be the same as the password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; init; } = null!;

    /// <summary>
    /// Gets the email of the registered user.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; init; } = null!;
}
