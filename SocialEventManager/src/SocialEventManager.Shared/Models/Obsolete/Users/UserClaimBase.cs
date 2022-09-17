using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public abstract record UserClaimBase
{
    public int Id { get; init; }

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.Length255)]
    public string Type { get; init; } = null!;

    [Required]
    [MinLength(LengthConstants.Length2)]
    [MaxLength(LengthConstants.LengthMax)]
    public string Value { get; init; } = null!;
}
