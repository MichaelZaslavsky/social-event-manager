// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

using Dapper.Contrib.Extensions;

public abstract class ClaimBase
{
    [Computed]
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string Type { get; set; } = null!;

    [Required]
    [StringLength(StringLengthAttribute.MaxText)]
    public string Value { get; set; } = null!;
}
*/
