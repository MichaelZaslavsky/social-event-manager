using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Shared.Models.Accounts;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public abstract record AccountBase
{
    [Required]
    [NotDefault]
    [ExplicitKey]
    public Guid UserId { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string? UserName { get; init; }

    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.LengthMax)]
    required public string PasswordHash { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string? Email { get; init; }

    [Required]
    public bool EmailConfirmed { get; init; }

    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.LengthMax)]
    public string? PhoneNumber { get; init; }

    [Required]
    public bool PhoneNumberConfirmed { get; init; }

    public DateTime? LockoutEnd { get; init; }

    [Required]
    public bool LockoutEnabled { get; init; }

    [Required]
    public int AccessFailedCount { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    required public string NormalizedEmail { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    required public string NormalizedUserName { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    required public string ConcurrencyStamp { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.LengthMax)]
    required public string SecurityStamp { get; init; }

    [Required]
    public bool TwoFactorEnabled { get; init; }
}
