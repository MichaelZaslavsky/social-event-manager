using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.BLL.Models.Accounts
{
    public abstract class AccountBase
    {
        [Required]
        [NotDefault]
        [ExplicitKey]
        public Guid UserId { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string UserName { get; set; }

        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.LengthMax)]
        public string PasswordHash { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string Email { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.LengthMax)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool PhoneNumberConfirmed { get; set; }

        public DateTime? LockoutEnd { get; set; }

        [Required]
        public bool LockoutEnabled { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string NormalizedEmail { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string NormalizedUserName { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string ConcurrencyStamp { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.LengthMax)]
        public string SecurityStamp { get; set; }

        [Required]
        public bool TwoFactorEnabled { get; set; }
    }
}
