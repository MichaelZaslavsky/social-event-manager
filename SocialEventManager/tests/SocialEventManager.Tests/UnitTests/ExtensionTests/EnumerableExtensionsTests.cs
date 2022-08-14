using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.DataMembers.Common;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

[UnitTest]
[Category(CategoryConstants.Extensions)]
public class EnumerableExtensionsTests
{
    [Theory]
    [MemberData(nameof(EnumerableData.EmptyData), MemberType = typeof(EnumerableData))]
    public void IsEmpty_Should_ReturnCorrectResult_When_Called(IEnumerable<int> enumerable, bool expectedResult)
    {
        bool actualResult = enumerable.IsEmpty();
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(null)]
    public void IsEmpty_Should_ReturnArgumentNullException_When_ValueIsNull<T>(IEnumerable<T> enumerable)
    {
        Action action = () => enumerable.IsEmpty();
        action.Should().Throw<ArgumentNullException>().WithMessage(ExceptionConstants.ValueCannotBeNull("source"));
    }

    [Theory]
    [MemberData(nameof(EnumerableData.NullOrEmptyData), MemberType = typeof(EnumerableData))]
    public void IsNullOrEmpty_Should_ReturnCorrectResult_When_Called(IEnumerable<int>? enumerable, bool expectedResult)
    {
        bool actualResult = enumerable.IsNullOrEmpty();
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(EnumerableData.NotNullAndAnyData), MemberType = typeof(EnumerableData))]
    public void IsNotNullAndAny_Should_ReturnCorrectResult_When_Called(IEnumerable<int>? enumerable, bool expectedResult)
    {
        bool actualResult = enumerable.IsNotNullAndAny();
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(EnumerableData.UpdateInForEachData), MemberType = typeof(EnumerableData))]
    public void ForEach_Should_UpdateAllValues_When_RoleNameIsUpdated(IEnumerable<Role> roles, string roleName)
    {
        foreach (Role actualRole in roles.ForEach(r => r.Name = roleName))
        {
            actualRole.Name.Should().Be(roleName);
        }

        foreach (Role role in roles)
        {
            role.Name.Should().Be(roleName);
        }
    }
}
