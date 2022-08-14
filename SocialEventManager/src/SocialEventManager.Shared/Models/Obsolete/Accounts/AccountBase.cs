// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Shared.Models.Accounts;

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
    public string PasswordHash { get; init; } = null!;

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
    public string NormalizedEmail { get; init; } = null!;

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string NormalizedUserName { get; init; } = null!;

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string ConcurrencyStamp { get; init; } = null!;

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.LengthMax)]
    public string SecurityStamp { get; init; } = null!;

    [Required]
    public bool TwoFactorEnabled { get; init; }
}
*/
