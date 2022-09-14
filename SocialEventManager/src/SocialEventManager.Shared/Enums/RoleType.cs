using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace SocialEventManager.Shared.Enums;

[EnumExtensions]
public enum RoleType
{
    [Description(nameof(Admin))]
    Admin = 0,

    [Description(nameof(User))]
    User = 1,
}
