using System;

namespace SocialEventManager.BLL.Models
{
    public class UserRoleDto : BaseUserRoleDto
    {
        public UserRoleDto(string userId, string roleName)
            : base(userId, roleName)
        {
        }

        public UserRoleDto(Guid userId, string roleName)
            : base(userId, roleName)
        {
        }
    }
}