using System;
using System.Reflection;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static MethodInfo GetNonPublicStaticMethod(this Type type, string methodName)
        {
            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);

            if (methodInfo == null)
            {
                throw new ArgumentOutOfRangeException(ExceptionConstants.MethodIsNotFound(methodName, nameof(type)));
            }

            return methodInfo;
        }
    }
}
