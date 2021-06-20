using System;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    [Table(TableNameConstants.UserRoles)]
    public class UserRole
    {
        public Guid UserId { get; set; }

        public int RoleId { get; set; }
    }
}
