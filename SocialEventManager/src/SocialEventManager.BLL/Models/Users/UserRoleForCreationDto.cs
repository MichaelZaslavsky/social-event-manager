namespace SocialEventManager.BLL.Models.Users;

public record UserRoleForCreationDto : UserRoleBase
{
    public UserRoleForCreationDto(string userId, string roleName)
        : base(userId, roleName)
    {
    }
}
