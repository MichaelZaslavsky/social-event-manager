namespace SocialEventManager.BLL.Models.Users
{
    public class UserRoleForCreationDto : UserRoleBase
    {
        public UserRoleForCreationDto(string userId, string roleName)
            : base(userId, roleName)
        {
        }
    }
}
