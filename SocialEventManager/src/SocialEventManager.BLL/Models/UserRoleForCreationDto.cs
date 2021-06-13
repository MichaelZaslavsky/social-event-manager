namespace SocialEventManager.BLL.Models
{
    public class UserRoleForCreationDto : BaseUserRoleDto
    {
        public UserRoleForCreationDto(string userId, string roleName)
            : base(userId, roleName)
        {
        }
    }
}
