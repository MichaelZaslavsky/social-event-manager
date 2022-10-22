using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Entities;

using ServiceStack.DataAnnotations;

[Table(TableNameConstants.UserRoles)]
[Alias(nameof(TableNameConstants.UserRoles))]
[UniqueConstraint(nameof(UserId), nameof(RoleId))]
[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class UserRole
{
    [Computed]
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    [Required]
    [ForeignKey(typeof(Account), OnDelete = GlobalConstants.Cascade)]
    required public Guid UserId { get; set; }

    [Required]
    [ForeignKey(typeof(Role), OnDelete = GlobalConstants.Cascade)]
    required public Guid RoleId { get; set; }
}
