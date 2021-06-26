using System;

namespace SocialEventManager.BLL.Models.Users
{
    public class UserRoleDto : UserRoleBase
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
