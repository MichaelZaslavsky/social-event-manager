// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Users;

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
*/
