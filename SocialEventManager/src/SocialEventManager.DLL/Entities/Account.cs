using System;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    [Table(TableNameConstants.Accounts)]
    public class Account
    {
        [ExplicitKey]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string NormalizedEmail { get; set; }

        public string NormalizedUserName { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string SecurityStamp { get; set; }

        public bool TwoFactorEnabled { get; set; }
    }
}
