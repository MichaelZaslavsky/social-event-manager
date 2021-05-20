using System;
using System.Net;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests
{
    [UnitTest]
    [Category(CategoryConstants.Extensions)]
    public class ExceptionExtensionsTests
    {
        [Theory]
        [MemberData(nameof(ExceptionData.ExceptionDataForHttpStatusAndTitle), MemberType = typeof(ExceptionData))]
        public void ToHttpStatusCodeAndTitle(Exception ex, (HttpStatusCode, string) expectedResult)
        {
            (HttpStatusCode, string) actualResult = ex.ToHttpStatusCodeAndTitle();
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(ExceptionData.CriticalExceptionsData), MemberType = typeof(ExceptionData))]
        public void IsCritical(Exception ex, bool expectedResult)
        {
            bool actualResult = ex.IsCritical();
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
