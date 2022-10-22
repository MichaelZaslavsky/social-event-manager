using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

[Table(TableNameConstants.UserClaims)]
[Alias(nameof(TableNameConstants.UserClaims))]
[UniqueConstraint(nameof(UserId), nameof(Type))]
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class UserClaim : ClaimBase
{
    [Required]
    [ForeignKey(typeof(Account), OnDelete = GlobalConstants.Cascade)]
    required public Guid UserId { get; set; }
}
