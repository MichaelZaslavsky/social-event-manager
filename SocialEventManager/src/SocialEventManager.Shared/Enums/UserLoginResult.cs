using System.ComponentModel;
using NetEscapades.EnumGenerators;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Enums;

[EnumExtensions]
public enum UserLoginResult
{
    Success = 0,

    [Description(AuthConstants.EmailOrPasswordIsIncorrect)]
    EmailNotFound = 1,

    [Description(AuthConstants.EmailOrPasswordIsIncorrect)]
    IncorrectPassword = 2,

    [Description(AuthConstants.EmailNotVerified)]
    EmailNotVerified = 3,

    [Description(AuthConstants.UserIsLocked)]
    Locked = 4,
}
