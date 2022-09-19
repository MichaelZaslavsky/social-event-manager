using System.Reflection;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Extensions;

public static class TypeExtensions
{
    public static MethodInfo GetNonPublicStaticMethod(this Type type, string methodName)
    {
        MethodInfo? methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        return methodInfo ?? throw new ArgumentOutOfRangeException(methodName, ExceptionConstants.MethodIsNotFound(methodName, nameof(type)));
    }
}
