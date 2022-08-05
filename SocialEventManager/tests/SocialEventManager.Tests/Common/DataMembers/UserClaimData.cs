using System.Security.Claims;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

public static class UserClaimData
{
    private const string TableName = TableNameConstants.UserClaims;

    private static readonly string Length256;

    static UserClaimData()
    {
        Length256 = DataConstants.Length256;
    }

    public static TheoryData<UserClaim> ValidUserClaim =>
        new() { GetMockUserClaim(userId: Guid.NewGuid()) };

    public static TheoryData<IEnumerable<UserClaim>> UserClaimsWithSameUser =>
        new() { GetMockUserClaims(sameUserId: true) };

    public static TheoryData<IEnumerable<UserClaim>> UserClaimsWithSameType =>
        new() { GetMockUserClaims(sameType: true, sameValue: true) };

    public static TheoryData<IEnumerable<UserClaim>> UserClaimsWithSameTypeAndValue =>
        new() { GetMockUserClaims(sameType: true, sameValue: true) };

    public static TheoryData<IEnumerable<UserClaim>> UserClaimsWithSameUserAndType =>
        new() { GetMockUserClaims(sameUserId: true, sameType: true) };

    public static TheoryData<UserClaim> UserClaimWithValidLength =>
        new()
        {
            { GetMockUserClaim(typeLength: LengthConstants.Length255) },
            { GetMockUserClaim(valueLength: LengthConstants.LengthMax) },
        };

    public static TheoryData<UserClaim, string> UserClaimWithMissingRequiredFields =>
        new()
        {
            {
                GetMockUserClaim(nullifyType: true),
                ExceptionConstants.CannotInsertTheValueNull(nameof(UserClaim.Type), TableName)
            },
            {
                GetMockUserClaim(nullifyValue: true),
                ExceptionConstants.CannotInsertTheValueNull(nameof(UserClaim.Value), TableName)
            },
        };

    public static TheoryData<UserClaim, string> UserClaimWithExceededLength =>
        new()
        {
            {
                GetMockUserClaim(type: Length256),
                ExceptionConstants.ExceedMaximumAllowedLength
            },
        };

    public static TheoryData<UserClaim, UserClaim> UserClaimsWithDifferentIds =>
        new()
        {
            {
                GetMockUserClaim(Guid.Empty, value: DataConstants.RandomText, id: 1),
                GetMockUserClaim(Guid.Empty, value: DataConstants.RandomText, id: 2)
            },
        };

    public static TheoryData<UserClaim?, UserClaim?> UserClaimsWithOneNull =>
        new()
        {
            {
                GetMockUserClaim(userId: Guid.NewGuid()),
                null
            },
            {
                null,
                GetMockUserClaim(userId: Guid.NewGuid())
            },
        };

    #region Private Methods

    public static UserClaim GetMockUserClaim(Guid? userId = null, string type = ClaimTypes.Name, string? value = null, int id = 1)
    {
        return new()
        {
            Id = id,
            UserId = userId ?? Guid.NewGuid(),
            Type = type,
            Value = value ?? RandomGeneratorHelpers.GenerateRandomValue(),
        };
    }

    private static IEnumerable<UserClaim> GetMockUserClaims(bool sameUserId = false, bool sameType = false, bool sameValue = false, int itemsCount = 2)
    {
        Guid userId = sameUserId ? Guid.NewGuid() : Guid.Empty;
        string? type = sameType ? ClaimTypes.Name : null;
        string? value = sameValue ? RandomGeneratorHelpers.GenerateRandomValue() : null;

        var userClaims = new UserClaim[itemsCount];

        for (int i = 0; i < itemsCount; i++)
        {
            userClaims[i] = new()
            {
                Id = i + 1,
                UserId = sameUserId ? userId : Guid.NewGuid(),
                Type = sameType ? type! : RandomGeneratorHelpers.GenerateRandomValue(),
                Value = sameValue ? value! : RandomGeneratorHelpers.GenerateRandomValue(),
            };
        }

        return userClaims;
    }

    private static UserClaim GetMockUserClaim(bool nullifyType = false, bool nullifyValue = false) =>
        new()
        {
            Id = RandomGeneratorHelpers.NextInt32(),
            UserId = Guid.NewGuid(),
            Type = nullifyType ? null! : ClaimTypes.Name,
            Value = nullifyValue ? null! : RandomGeneratorHelpers.GenerateRandomValue(),
        };

    private static UserClaim GetMockUserClaim(int typeLength = LengthConstants.Length255, int valueLength = LengthConstants.Length255) =>
        new()
        {
            Id = RandomGeneratorHelpers.NextInt32(),
            UserId = Guid.NewGuid(),
            Type = RandomGeneratorHelpers.GenerateRandomValue(typeLength),
            Value = RandomGeneratorHelpers.GenerateRandomValue(valueLength),
        };

    #endregion Private Methods
}
