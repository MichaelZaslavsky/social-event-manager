using System;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class AccountData
    {
        public static Account GetMockAccount(Guid? userId = null, string userName = null, string passwordHash = null, string email = null, bool emailConfirmed = false,
            string phoneNumber = null, bool phoneNumberConfirmed = false, DateTime? lockoutEnd = null, bool lockoutEnabled = false, int accessFailedCount = 0,
            string concurrencyStamp = null, string securityStamp = null, bool twoFactorEnabled = false)
        {
            email ??= $"{RandomGeneratorHelpers.GenerateRandomValue()}@gmail.com";
            userName ??= RandomGeneratorHelpers.GenerateRandomValue();

            return new Account
            {
                UserId = userId ?? Guid.NewGuid(),
                UserName = userName,
                PasswordHash = passwordHash ?? Guid.NewGuid().ToString(),
                Email = email,
                EmailConfirmed = emailConfirmed,
                PhoneNumber = phoneNumber ?? DataConstants.PhoneNumber,
                PhoneNumberConfirmed = phoneNumberConfirmed,
                LockoutEnd = lockoutEnd,
                LockoutEnabled = lockoutEnabled,
                AccessFailedCount = accessFailedCount,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = userName.ToUpper(),
                ConcurrencyStamp = concurrencyStamp ?? Guid.NewGuid().ToString(),
                SecurityStamp = securityStamp ?? Guid.NewGuid().ToString(),
                TwoFactorEnabled = twoFactorEnabled,
            };
        }
    }
}
