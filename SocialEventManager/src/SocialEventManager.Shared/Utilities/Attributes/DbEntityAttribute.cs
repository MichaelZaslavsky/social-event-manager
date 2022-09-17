using SocialEventManager.DAL.Utilities.Enums;

namespace SocialEventManager.Shared.Utilities.Attributes;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public sealed class DbEntityAttribute : Attribute
{
    public DbEntityAttribute(DbTypes dbTypes)
    {
        DbTypes = dbTypes;
    }

    public DbTypes DbTypes { get; }
}
