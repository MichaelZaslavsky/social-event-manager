using System;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    using Dapper.Contrib.Extensions;

    [Table(TableNameConstants.UserRoles)]
    [Alias(AliasConstants.UserRoles)]
    [UniqueConstraint(nameof(UserId), nameof(RoleId))]
    public class UserRole
    {
        [Computed]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Account), OnDelete = GlobalConstants.Cascade)]
        public Guid UserId { get; set; }

        [ForeignKey(typeof(Role), OnDelete = GlobalConstants.Cascade)]
        public Guid RoleId { get; set; }
    }
}
