using System.Diagnostics.CodeAnalysis;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

using Dapper.Contrib.Extensions;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public abstract class ClaimBase
{
    [Computed]
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    [Required]
    [StringLength(LengthConstants.Length255)]
    required public string Type { get; set; }

    [Required]
    [StringLength(StringLengthAttribute.MaxText)]
    required public string Value { get; set; }
}
