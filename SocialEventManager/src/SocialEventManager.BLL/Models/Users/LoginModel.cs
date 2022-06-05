using System.ComponentModel.DataAnnotations;

namespace SocialEventManager.BLL.Models.Users;

/// <summary>
/// The details for the user login.
/// </summary>
public record LoginModel
{
    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    [Required]
    public string UserName { get; init; }

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }
}
