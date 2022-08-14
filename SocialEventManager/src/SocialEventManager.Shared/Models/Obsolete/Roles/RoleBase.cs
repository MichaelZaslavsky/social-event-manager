// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Shared.Models.Roles;

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
*/
