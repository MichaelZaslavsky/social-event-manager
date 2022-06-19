namespace SocialEventManager.Shared.Utilities.Attributes;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class EnumTableAttribute : Attribute
{
    public EnumTableAttribute(string tableName)
    {
        TableName = tableName;
    }

    public string TableName { get; }
}
