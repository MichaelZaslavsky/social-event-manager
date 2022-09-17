// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

using Dapper.Contrib.Extensions;

[Table(TableNameConstants.UserRoles)]
[Alias(nameof(TableNameConstants.UserRoles))]
[UniqueConstraint(nameof(UserId), nameof(RoleId))]
public sealed class UserRole
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
*/
