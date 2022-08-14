// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

using System.ComponentModel.DataAnnotations;

namespace SocialEventManager.Shared.Models.Users;

/// <summary>
/// The details for the user login.
/// </summary>
public record LoginModel
{
    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    [Required]
    public string UserName { get; init; } = null!;

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = null!;
}
