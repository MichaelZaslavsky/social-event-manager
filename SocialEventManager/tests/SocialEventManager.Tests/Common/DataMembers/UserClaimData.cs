using System;
using System.Collections.Generic;
using System.Security.Claims;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class UserClaimData
    {
        private const string TableName = TableNameConstants.UserClaims;

        public static IEnumerable<object[]> ValidUserClaim
        {
            get
            {
                yield return new object[] { GetMockUserClaim(userId: Guid.NewGuid()) };
            }
        }

        public static IEnumerable<object[]> UserClaimsWithSameUser
        {
            get
            {
                yield return new object[] { GetMockUserClaims(sameUserId: true) };
            }
        }

        public static IEnumerable<object[]> UserClaimsWithSameType
        {
            get
            {
                yield return new object[] { GetMockUserClaims(sameType: true, sameValue: true) };
            }
        }

        public static IEnumerable<object[]> UserClaimsWithSameTypeAndValue
        {
            get
            {
                yield return new object[] { GetMockUserClaims(sameType: true, sameValue: true) };
            }
        }

        public static IEnumerable<object[]> UserClaimsWithSameUserAndType
        {
            get
            {
                yield return new object[] { GetMockUserClaims(sameUserId: true, sameType: true) };
            }
        }

        public static IEnumerable<object[]> UserClaimWithValidLength
        {
            get
            {
                yield return new object[]
                {
                    GetMockUserClaim(typeLength: LengthConstants.Length255),
                };
            }
        }

        public static IEnumerable<object[]> UserClaimWithMissingRequiredFields
        {
            get
            {
                yield return new object[]
                {
                    GetMockUserClaim(nullifyType: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(UserClaim.Type), TableName),
                };
                yield return new object[]
                {
                    GetMockUserClaim(nullifyValue: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(UserClaim.Value), TableName),
                };
            }
        }

        public static IEnumerable<object[]> UserClaimWithExceededLength
        {
            get
            {
                yield return new object[]
                {
                    GetMockUserClaim(typeLength: LengthConstants.Length255 + 1),
                    ExceptionConstants.StringExccedsMaximumLengthAllowed,
                };
            }
        }

        #region Private Methods

        private static UserClaim GetMockUserClaim(Guid? userId = null, string type = ClaimTypes.Name)
        {
            return new UserClaim
            {
                Id = RandomGeneratorHelpers.NextInt32(),
                UserId = userId ?? Guid.NewGuid(),
                Type = type,
                Value = RandomGeneratorHelpers.GenerateRandomValue(),
            };
        }

        private static IEnumerable<UserClaim> GetMockUserClaims(bool sameUserId = false, bool sameType = false, bool sameValue = false, int itemsCount = 2)
        {
            Guid userId = sameUserId ? Guid.NewGuid() : Guid.Empty;
            string type = sameType ? ClaimTypes.Name : null;
            string value = sameValue ? RandomGeneratorHelpers.GenerateRandomValue() : null;

            var userClaims = new UserClaim[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                userClaims[i] = new UserClaim
                {
                    Id = i + 1,
                    UserId = sameUserId ? userId : Guid.NewGuid(),
                    Type = sameType ? type : RandomGeneratorHelpers.GenerateRandomValue(),
                    Value = sameValue ? value : RandomGeneratorHelpers.GenerateRandomValue(),
                };
            }

            return userClaims;
        }

        private static UserClaim GetMockUserClaim(bool nullifyType = false, bool nullifyValue = false) =>
            new()
            {
                Id = RandomGeneratorHelpers.NextInt32(),
                UserId = Guid.NewGuid(),
                Type = nullifyType ? null : ClaimTypes.Name,
                Value = nullifyValue ? null : RandomGeneratorHelpers.GenerateRandomValue(),
            };

        private static UserClaim GetMockUserClaim(int typeLength = LengthConstants.Length255) =>
            new()
            {
                Id = RandomGeneratorHelpers.NextInt32(),
                UserId = Guid.NewGuid(),
                Type = RandomGeneratorHelpers.GenerateRandomValue(typeLength),
                Value = RandomGeneratorHelpers.GenerateRandomValue(),
            };

        #endregion Private Methods
    }
}
