namespace SocialEventManager.BLL.Models.Users
{
    public class UserRoleForCreationDto : BaseUserRoleDto
    {
        public UserRoleForCreationDto(string userId, string roleName)
            : base(userId, roleName)
        {
        }
    }
}
