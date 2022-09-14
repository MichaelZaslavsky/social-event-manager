using System.Globalization;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

internal static class RoleData
{
    private const string TableName = TableNameConstants.Roles;

    public static TheoryData<Role> ValidRole =>
        new()
        {
            { GetMockRole(RoleType.User.GetDescription()) },
            { GetMockRole(RoleType.Admin.GetDescription()) },
        };

    public static TheoryData<List<Role>> ValidRoles =>
        new()
        {
            new()
            {
                GetMockRole(RoleType.User.GetDescription()),
                GetMockRole(RoleType.Admin.GetDescription()),
            },
        };

    public static TheoryData<List<Role>> RolesWithSameName =>
        new()
        {
            new()
            {
                GetMockRole(RoleType.User.GetDescription()),
                GetMockRole(RoleType.User.GetDescription()),
            },
        };

    public static TheoryData<Role> RoleWithValidLength =>
        new()
        {
            { GetMockRole(concurrencyStampLength: LengthConstants.Length255) },
            { GetMockRole(nameLength: LengthConstants.Length255) },
            { GetMockRole(normalizedNameLength: LengthConstants.Length255) },
        };

    public static TheoryData<Role, string> RoleWithMissingRequiredFields =>
        new()
        {
            {
                GetMockRole(nullifyConcurrencyStamp: true),
                ExceptionConstants.CannotInsertTheValueNull(nameof(Role.ConcurrencyStamp), TableName)
            },
            {
                GetMockRole(nullifyName: true),
                ExceptionConstants.CannotInsertTheValueNull(nameof(Role.Name), TableName)
            },
            {
                GetMockRole(nullifyNormalizedName: true),
                ExceptionConstants.CannotInsertTheValueNull(nameof(Role.NormalizedName), TableName)
            },
        };

    public static TheoryData<Role, string> RoleWithExceededLength =>
        new()
        {
            {
                GetMockRole(concurrencyStamp: TestConstants.Length256),
                ExceptionConstants.ExceedMaximumAllowedLength
            },
            {
                GetMockRole(name: TestConstants.Length256),
                ExceptionConstants.ExceedMaximumAllowedLength
            },
            {
                GetMockRole(normalizedName: TestConstants.Length256),
                ExceptionConstants.ExceedMaximumAllowedLength
            },
        };

    public static Role GetMockRole(string name = "User", Guid? id = null, string? concurrencyStamp = null, string? normalizedName = null) =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            ConcurrencyStamp = concurrencyStamp ?? Guid.NewGuid().ToString().ToLower(CultureInfo.InvariantCulture),
            Name = name,
            NormalizedName = normalizedName ?? name.ToUpper(CultureInfo.InvariantCulture),
        };

    #region Private Methods

    private static Role GetMockRole(bool nullifyConcurrencyStamp = false, bool nullifyName = false, bool nullifyNormalizedName = false) =>
        new()
        {
            Id = Guid.NewGuid(),
            ConcurrencyStamp = nullifyConcurrencyStamp ? null! : Guid.NewGuid().ToString().ToLower(CultureInfo.InvariantCulture),
            Name = nullifyName ? null! : RoleType.User.GetDescription(),
            NormalizedName = nullifyNormalizedName ? null! : RoleType.User.GetDescription().ToUpper(CultureInfo.InvariantCulture),
        };

    private static Role GetMockRole(
        int concurrencyStampLength = LengthConstants.Length255, int nameLength = LengthConstants.Length255, int normalizedNameLength = LengthConstants.Length255) =>
        new()
        {
            Id = Guid.NewGuid(),
            ConcurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(concurrencyStampLength),
            Name = RandomGeneratorHelpers.GenerateRandomValue(nameLength),
            NormalizedName = RandomGeneratorHelpers.GenerateRandomValue(normalizedNameLength),
        };

    #endregion Private Methods
}
