using System.Globalization;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class AccountData
    {
        private const string TableName = TableNameConstants.Accounts;

        private static readonly string Length256;

        static AccountData()
        {
            Length256 = DataConstants.Length256;
        }

        public static IEnumerable<object[]> AccountWithSameUserId
        {
            get
            {
                yield return new object[] { GetMockAccount(sameUserId: true) };
            }
        }

        public static IEnumerable<object[]> AccountWithSameEmail
        {
            get
            {
                yield return new object[] { GetMockAccount(sameEmail: true) };
            }
        }

        public static IEnumerable<object[]> AccountWithValidLength
        {
            get
            {
                yield return new object[]
                {
                    GetMockAccount(emailLength: LengthConstants.Length255),
                };
                yield return new object[]
                {
                    GetMockAccount(normalizedEmailLength: LengthConstants.Length255),
                };
                yield return new object[]
                {
                    GetMockAccount(userNameLength: LengthConstants.Length255),
                };
                yield return new object[]
                {
                    GetMockAccount(normalizedUserNameLength: LengthConstants.Length255),
                };
                yield return new object[]
                {
                    GetMockAccount(concurrencyStampLength: LengthConstants.Length255),
                };
                yield return new object[]
                {
                    GetMockAccount(passwordHashLength: LengthConstants.LengthMax),
                };
                yield return new object[]
                {
                    GetMockAccount(phoneNumberLength: LengthConstants.LengthMax),
                };
                yield return new object[]
                {
                    GetMockAccount(securityStampLength: LengthConstants.LengthMax),
                };
            }
        }

        public static IEnumerable<object[]> AccountWithNonRequiredNullField
        {
            get
            {
                yield return new object[]
                {
                    GetMockAccount(nullifyPasswordHash: true),
                };
            }
        }

        public static IEnumerable<object[]> AccountWithMissingRequiredFields
        {
            get
            {
                yield return new object[]
                {
                    GetMockAccount(nullifyEmail: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Account.Email), TableName),
                };
                yield return new object[]
                {
                    GetMockAccount(nullifyNormalizedEmail: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Account.NormalizedEmail), TableName),
                };
                yield return new object[]
                {
                    GetMockAccount(nullifyUserName: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Account.UserName), TableName),
                };
                yield return new object[]
                {
                    GetMockAccount(nullifyNormalizedUserName: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Account.NormalizedUserName), TableName),
                };
                yield return new object[]
                {
                    GetMockAccount(nullifyConcurrencyStamp: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Account.ConcurrencyStamp), TableName),
                };
                yield return new object[]
                {
                    GetMockAccount(nullifySecurityStamp: true),
                    ExceptionConstants.CannotInsertTheValueNull(nameof(Account.SecurityStamp), TableName),
                };
            }
        }

        public static IEnumerable<object[]> AccountWithExceededLength
        {
            get
            {
                yield return new object[]
                {
                    GetMockAccount(email: Length256),
                    ExceptionConstants.ExceedMaximumAllowedLength,
                };
                yield return new object[]
                {
                    GetMockAccount(normalizedEmail: Length256),
                    ExceptionConstants.ExceedMaximumAllowedLength,
                };
                yield return new object[]
                {
                    GetMockAccount(userName: Length256),
                    ExceptionConstants.ExceedMaximumAllowedLength,
                };
                yield return new object[]
                {
                    GetMockAccount(normalizedUserName: Length256),
                    ExceptionConstants.ExceedMaximumAllowedLength,
                };
                yield return new object[]
                {
                    GetMockAccount(concurrencyStamp: Length256),
                    ExceptionConstants.ExceedMaximumAllowedLength,
                };
            }
        }

        public static Account GetMockAccount(Guid? userId = null, int id = 1, string userName = null, string passwordHash = null, string email = null,
            bool emailConfirmed = false, string phoneNumber = null, bool phoneNumberConfirmed = false, DateTime? lockoutEnd = null, bool lockoutEnabled = false,
            int accessFailedCount = 0, string concurrencyStamp = null, string securityStamp = null, bool twoFactorEnabled = false,
            string normalizedEmail = null, string normalizedUserName = null)
        {
            email ??= $"{RandomGeneratorHelpers.GenerateRandomValue()}@gmail.com";
            userName ??= RandomGeneratorHelpers.GenerateRandomValue();

            return new Account
            {
                Id = id,
                UserId = userId ?? Guid.NewGuid(),
                UserName = userName ?? RandomGeneratorHelpers.GenerateRandomValue(),
                PasswordHash = passwordHash ?? Guid.NewGuid().ToString(),
                Email = email,
                EmailConfirmed = emailConfirmed,
                PhoneNumber = phoneNumber ?? DataConstants.PhoneNumber,
                PhoneNumberConfirmed = phoneNumberConfirmed,
                LockoutEnd = lockoutEnd,
                LockoutEnabled = lockoutEnabled,
                AccessFailedCount = accessFailedCount,
                NormalizedEmail = normalizedEmail ?? email.ToUpper(CultureInfo.InvariantCulture),
                NormalizedUserName = normalizedUserName ?? userName.ToUpper(CultureInfo.InvariantCulture),
                ConcurrencyStamp = concurrencyStamp ?? Guid.NewGuid().ToString(),
                SecurityStamp = securityStamp ?? Guid.NewGuid().ToString(),
                TwoFactorEnabled = twoFactorEnabled,
            };
        }

        #region Private Methods

        private static IEnumerable<Account> GetMockAccount(bool sameUserId = false, bool sameEmail = false, int itemsCount = 2)
        {
            Guid? userId = sameUserId ? Guid.NewGuid() : null;
            string email = sameEmail ? $"{RandomGeneratorHelpers.GenerateRandomValue()}@gmail.com" : null;

            var accounts = new Account[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                accounts[i] = GetMockAccount(userId, i + 1, email: email);
            }

            return accounts;
        }

        private static Account GetMockAccount(bool nullifyEmail = false, bool nullifyNormalizedEmail = false, bool nullifyUserName = false,
            bool nullifyNormalizedUserName = false, bool nullifyConcurrencyStamp = false, bool nullifySecurityStamp = false, bool nullifyPasswordHash = false)
        {
            string email = nullifyEmail ? null : $"{RandomGeneratorHelpers.GenerateRandomValue()}@gmail.com";
            string normalizedEmail = nullifyNormalizedEmail ? null : $"{RandomGeneratorHelpers.GenerateRandomValue()}@gmail.com".ToUpper(CultureInfo.InvariantCulture);
            string userName = nullifyUserName ? null : RandomGeneratorHelpers.GenerateRandomValue();
            string normalizedUserName = nullifyNormalizedUserName ? null : RandomGeneratorHelpers.GenerateRandomValue();
            string concurrencyStamp = nullifyConcurrencyStamp ? null : Guid.NewGuid().ToString();
            string securityStamp = nullifySecurityStamp ? null : Guid.NewGuid().ToString();
            string passwordHash = nullifyPasswordHash ? null : Guid.NewGuid().ToString();

            return new Account
            {
                Id = 1,
                UserId = Guid.NewGuid(),
                UserName = userName,
                PasswordHash = passwordHash,
                Email = email,
                EmailConfirmed = false,
                PhoneNumber = DataConstants.PhoneNumber,
                PhoneNumberConfirmed = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                NormalizedEmail = normalizedEmail,
                NormalizedUserName = normalizedUserName,
                ConcurrencyStamp = concurrencyStamp,
                SecurityStamp = securityStamp,
                TwoFactorEnabled = false,
            };
        }

        private static Account GetMockAccount(int emailLength = LengthConstants.Length255, int normalizedEmailLength = LengthConstants.Length255,
            int userNameLength = LengthConstants.Length255, int normalizedUserNameLength = LengthConstants.Length255,
            int concurrencyStampLength = LengthConstants.Length255, int passwordHashLength = LengthConstants.Length255,
            int phoneNumberLength = LengthConstants.Length255, int securityStampLength = LengthConstants.Length255)
        {
            const int numberOfEmailExtensionCharacters = 10;
            string email = $"{RandomGeneratorHelpers.GenerateRandomValue(emailLength - numberOfEmailExtensionCharacters)}@gmail.com";

            string normalizedEmail = $"{RandomGeneratorHelpers.GenerateRandomValue(normalizedEmailLength - numberOfEmailExtensionCharacters)}@gmail.com"
                .ToUpper(CultureInfo.InvariantCulture);

            string userName = RandomGeneratorHelpers.GenerateRandomValue(userNameLength);
            string normalizedUserName = RandomGeneratorHelpers.GenerateRandomValue(normalizedUserNameLength);
            string concurrencyStamp = RandomGeneratorHelpers.GenerateRandomValue(concurrencyStampLength);
            string passwordHash = RandomGeneratorHelpers.GenerateRandomValue(passwordHashLength);
            string phoneNumber = RandomGeneratorHelpers.GenerateRandomValue(phoneNumberLength);
            string securityStamp = RandomGeneratorHelpers.GenerateRandomValue(securityStampLength);

            return new Account
            {
                Id = 1,
                UserId = Guid.NewGuid(),
                UserName = userName,
                PasswordHash = passwordHash,
                Email = email,
                EmailConfirmed = false,
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                NormalizedEmail = normalizedEmail,
                NormalizedUserName = normalizedUserName,
                ConcurrencyStamp = concurrencyStamp,
                SecurityStamp = securityStamp,
                TwoFactorEnabled = false,
            };
        }

        #endregion Private Methods
    }
}
