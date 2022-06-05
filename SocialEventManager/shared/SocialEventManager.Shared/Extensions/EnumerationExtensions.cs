using System.ComponentModel;
using System.Reflection;

namespace SocialEventManager.Shared.Extensions;

public static class EnumerationExtensions
{
    public static string GetDescription(this Enum value)
    {
        MemberInfo enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();

        DescriptionAttribute descriptionAttribute = enumMember is null
            ? default
            : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

        return descriptionAttribute is null
            ? value.ToString()
            : descriptionAttribute.Description;
    }
}
