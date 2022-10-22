using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

/// <summary>
/// The user data for registration.
/// </summary>
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed record RegisterUserDto
{
    /// <summary>
    /// Gets the name of the registered user.
    /// </summary>
    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    required public string UserName { get; init; }

    /// <summary>
    /// Gets the password of the registered user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    required public string Password { get; init; }

    /// <summary>
    /// Gets the confirmation password of the registered user.
    /// It should be the same as the password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    required public string ConfirmPassword { get; init; }

    /// <summary>
    /// Gets the email of the registered user.
    /// </summary>
    [Required]
    [EmailAddress]
    required public string Email { get; init; }
}
