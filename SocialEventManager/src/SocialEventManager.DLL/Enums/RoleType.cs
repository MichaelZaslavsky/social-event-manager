using System.ComponentModel;

namespace SocialEventManager.DLL.Enums
{
    public enum RoleType
    {
        [Description(nameof(Admin))]
        Admin = 0,

        [Description(nameof(User))]
        User = 1,
    }
}
