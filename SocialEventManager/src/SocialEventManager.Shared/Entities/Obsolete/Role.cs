using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

[Table(TableNameConstants.Roles)]
[Alias(nameof(TableNameConstants.Roles))]
[UniqueConstraint(nameof(Name))]
public sealed class Role
{
    [Required]
    [ExplicitKey]
    [PrimaryKey]
    required public Guid Id { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    required public string ConcurrencyStamp { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    required public string Name { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    required public string NormalizedName { get; set; }
}
