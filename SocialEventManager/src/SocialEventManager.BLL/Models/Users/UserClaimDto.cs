using System;
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.BLL.Models.Users
{
    public class UserClaimDto : UserClaimBase
    {
        [Required]
        [NotDefault]
        public Guid UserId { get; set; }
    }
}
