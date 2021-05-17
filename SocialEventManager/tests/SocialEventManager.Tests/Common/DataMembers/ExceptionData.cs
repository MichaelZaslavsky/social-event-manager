using System;
using System.Collections.Generic;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class ExceptionData
    {
        public static IEnumerable<object[]> CriticalExceptionsData
        {
            get
            {
                yield return new object[] { new OutOfMemoryException(), true };
                yield return new object[] { new AppDomainUnloadedException(), true };
                yield return new object[] { new BadImageFormatException(), true };
                yield return new object[] { new CannotUnloadAppDomainException(), true };
                yield return new object[] { new InvalidProgramException(), true };
                yield return new object[] { new Exception(ExceptionConstants.CannotOpenDatabase), true };
                yield return new object[] { new Exception(ExceptionConstants.ANetworkRelated), true };
                yield return new object[] { null, false };
                yield return new object[] { new NullReferenceException(), false };
                yield return new object[] { new ArgumentNullException(), false };
            }
        }
    }
}
