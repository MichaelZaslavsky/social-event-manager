using System;
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.BLL.Models
{
    public class BaseRoleDto
    {
        [Required]
        [NotDefault]
        public Guid Id { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string ConcurrencyStamp { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string Name { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string NormalizedName { get; set; }
    }
}
