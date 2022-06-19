using System.ComponentModel;
using SocialEventManager.DAL.Utilities.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.DAL.Enums;

[DbEntity(DbTypes.SocialEventManager)]
[EnumTable(TableNameConstants.SchemaTypes)]
public enum SchemaType
{
    [Description(SchemaConstants.Default)]
    Default = 0,

    [Description(SchemaConstants.Migration)]
    Migration = 1,

    [Description(SchemaConstants.Enum)]
    Enum = 2,
}
