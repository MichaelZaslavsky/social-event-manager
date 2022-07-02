using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities;

[Table(TableNameConstants.UserClaims)]
[Alias(nameof(TableNameConstants.UserClaims))]
[UniqueConstraint(nameof(UserId), nameof(Type))]
public class UserClaim : ClaimBase
{
    [Required]
    [ForeignKey(typeof(Account), OnDelete = GlobalConstants.Cascade)]
    public Guid UserId { get; set; }
}
