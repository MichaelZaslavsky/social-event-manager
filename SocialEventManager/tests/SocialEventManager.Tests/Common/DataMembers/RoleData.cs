using System;
using System.Collections.Generic;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class RoleData
    {
        private const string RolesTableName = TableNameConstants.Roles;

        public static IEnumerable<object[]> ValidRole
        {
            get
            {
                yield return new object[] { GetMockRole(RoleType.User.GetDescription()) };
                yield return new object[] { GetMockRole(RoleType.Admin.GetDescription()) };
            }
        }

        public static IEnumerable<object[]> ValidRoles
        {
            get
            {
                yield return new object[]
                {
                    new List<Role>
                    {
                        GetMockRole(RoleType.User.GetDescription()),
                        GetMockRole(RoleType.Admin.GetDescription()),
                    },
                };
            }
        }

        public static IEnumerable<object[]> RolesWithSameName
        {
            get
            {
                yield return new object[]
                {
                    new List<Role>
                    {
                        GetMockRole(RoleType.User.GetDescription()),
                        GetMockRole(RoleType.User.GetDescription()),
                    },
                };
            }
        }

        public static IEnumerable<object[]> RoleWithMissingRequiredFields
        {
            get
            {
                yield return new object[]
                {
                    GetMockRole(nullifyConcurrencyStamp: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Role.ConcurrencyStamp), RolesTableName),
                };
                yield return new object[]
                {
                    GetMockRole(nullifyName: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Role.Name), RolesTableName),
                };
                yield return new object[]
                {
                    GetMockRole(nullifyNormalizedName: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Role.NormalizedName), RolesTableName),
                };
            }
        }

        public static IEnumerable<object[]> RoleWithExceededLength
        {
            get
            {
                yield return new object[]
                {
                    GetMockRole(concurrencyStampLength: LengthConstants.Length255 + 1),
                    ExceptionConstants.StringExccedsMaximumLengthAllowed,
                };
                yield return new object[]
                {
                    GetMockRole(nameLength: LengthConstants.Length255 + 1),
                    ExceptionConstants.StringExccedsMaximumLengthAllowed,
                };
                yield return new object[]
                {
                    GetMockRole(normalizedNameLength: LengthConstants.Length255 + 1),
                    ExceptionConstants.StringExccedsMaximumLengthAllowed,
                };
            }
        }

        public static IEnumerable<object[]> RoleWithValidLength
        {
            get
            {
                yield return new object[]
                {
                    GetMockRole(concurrencyStampLength: LengthConstants.Length255)
                };
                yield return new object[]
                {
                    GetMockRole(nameLength: LengthConstants.Length255)
                };
                yield return new object[]
                {
                    GetMockRole(normalizedNameLength: LengthConstants.Length255)
                };
            }
        }

        public static Role GetMockRole(string name = "User") =>
            new()
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = Guid.NewGuid().ToString().ToLower(),
                Name = name,
                NormalizedName = name.ToUpper(),
            };

        #region Private Methods

        private static Role GetMockRole(bool nullifyConcurrencyStamp = false, bool nullifyName = false, bool nullifyNormalizedName = false) =>
            new()
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = nullifyConcurrencyStamp ? null : Guid.NewGuid().ToString().ToLower(),
                Name = nullifyName ? null : RoleType.User.GetDescription(),
                NormalizedName = nullifyNormalizedName ? null : RoleType.User.GetDescription().ToUpper(),
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
}
