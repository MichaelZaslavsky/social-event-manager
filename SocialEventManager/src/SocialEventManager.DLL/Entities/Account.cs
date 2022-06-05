using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities;

using Dapper.Contrib.Extensions;

[Table(TableNameConstants.Accounts)]
[Alias(AliasConstants.Accounts)]
[UniqueConstraint(nameof(Email))]
[UniqueConstraint(nameof(UserId))]
public class Account
{
    [Computed]
    [AutoIncrement]
    public int Id { get; set; }

    [Required]
    [ExplicitKey]
    [PrimaryKey]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string UserName { get; set; } = null!;

    [StringLength(StringLengthAttribute.MaxText)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string Email { get; set; } = null!;

    [Required]
    public bool EmailConfirmed { get; set; }

    [StringLength(StringLengthAttribute.MaxText)]
    public string? PhoneNumber { get; set; }

    [Required]
    public bool PhoneNumberConfirmed { get; set; }

    public DateTime? LockoutEnd { get; set; }

    [Required]
    public bool LockoutEnabled { get; set; }

    [Required]
    public int AccessFailedCount { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string NormalizedEmail { get; set; } = null!;

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string NormalizedUserName { get; set; } = null!;

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string ConcurrencyStamp { get; set; } = null!;

    [Required]
    [StringLength(StringLengthAttribute.MaxText)]
    public string SecurityStamp { get; set; } = null!;

    [Required]
    public bool TwoFactorEnabled { get; set; }
}
