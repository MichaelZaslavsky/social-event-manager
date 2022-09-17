using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Shared.Models.Roles;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public abstract record RoleBase
{
    [Required]
    [NotDefault]
    public Guid Id { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string ConcurrencyStamp { get; init; } = null!;

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string Name { get; init; } = null!;

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string NormalizedName { get; init; } = null!;
}
