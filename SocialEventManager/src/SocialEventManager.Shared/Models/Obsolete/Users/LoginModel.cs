using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

/// <summary>
/// The details for the user login.
/// </summary>
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed record LoginModel
{
    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    [Required]
    required public string UserName { get; init; }

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    required public string Password { get; init; }
}
