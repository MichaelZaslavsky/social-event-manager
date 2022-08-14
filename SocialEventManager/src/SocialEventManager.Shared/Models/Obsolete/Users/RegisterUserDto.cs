// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

/// <summary>
/// The user data for registration.
/// </summary>
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
*/
