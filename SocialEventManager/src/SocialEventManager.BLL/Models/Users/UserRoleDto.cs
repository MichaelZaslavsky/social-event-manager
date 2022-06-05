namespace SocialEventManager.BLL.Models.Users;

public record UserRoleDto : UserRoleBase
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
