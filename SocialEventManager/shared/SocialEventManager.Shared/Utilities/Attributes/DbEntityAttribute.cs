using System;
using SocialEventManager.DLL.Utilities.Enums;

namespace SocialEventManager.Shared.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class DbEntityAttribute : Attribute
    {
        public DbEntityAttribute(DbTypes dbTypes)
        {
            DbTypes = dbTypes;
        }

        public DbTypes DbTypes { get; }
    }
}
