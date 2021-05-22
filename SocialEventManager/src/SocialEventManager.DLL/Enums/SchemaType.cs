using System.ComponentModel;
using SocialEventManager.DLL.Utilities.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.DLL.Enums
{
    [DbEntity(DbTypes.SocialEventManager)]
    public enum SchemaType
    {
        [Description(SchemaConstants.Default)]
        Default = 0,

        [Description(SchemaConstants.Migration)]
        Migration = 1,

        [Description(SchemaConstants.Enum)]
        Enum = 2,
    }
}
