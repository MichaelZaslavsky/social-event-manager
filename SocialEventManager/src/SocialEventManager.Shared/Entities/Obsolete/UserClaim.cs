// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

[Table(TableNameConstants.UserClaims)]
[Alias(nameof(TableNameConstants.UserClaims))]
[UniqueConstraint(nameof(UserId), nameof(Type))]
public class UserClaim : ClaimBase
{
    [Required]
    [ForeignKey(typeof(Account), OnDelete = GlobalConstants.Cascade)]
    public Guid UserId { get; set; }
}
*/
