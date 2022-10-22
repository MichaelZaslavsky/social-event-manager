using System.Diagnostics.CodeAnalysis;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

using Dapper.Contrib.Extensions;

[Table(TableNameConstants.Accounts)]
[Alias(nameof(TableNameConstants.Accounts))]
[UniqueConstraint(nameof(Email))]
[UniqueConstraint(nameof(UserId))]
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class Account
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
    required public string UserName { get; set; }

    [StringLength(StringLengthAttribute.MaxText)]
    required public string PasswordHash { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    required public string Email { get; set; }

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
    required public string NormalizedEmail { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    required public string NormalizedUserName { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    required public string ConcurrencyStamp { get; set; }

    [Required]
    [StringLength(StringLengthAttribute.MaxText)]
    required public string SecurityStamp { get; set; }

    [Required]
    public bool TwoFactorEnabled { get; set; }
}
