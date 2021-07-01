using System;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    using Dapper.Contrib.Extensions;

    [Table(TableNameConstants.UserRoles)]
    public class UserRole
    {
        [Computed]
        [AutoIncrement]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
