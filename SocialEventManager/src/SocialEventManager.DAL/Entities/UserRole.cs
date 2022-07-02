using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities;

using Dapper.Contrib.Extensions;

[Table(TableNameConstants.UserRoles)]
[Alias(nameof(TableNameConstants.UserRoles))]
[UniqueConstraint(nameof(UserId), nameof(RoleId))]
public class UserRole
{
    [Computed]
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    [Required]
    [ForeignKey(typeof(Account), OnDelete = GlobalConstants.Cascade)]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey(typeof(Role), OnDelete = GlobalConstants.Cascade)]
    public Guid RoleId { get; set; }
}
