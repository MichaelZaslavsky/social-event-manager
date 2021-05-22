using System;
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.BLL.Models
{
    // Temp class - for test purposes
    public class UserDto
    {
        public Guid ExternalId { get; set; }

        [Required]
        [MaxLength(LengthConstants.Length255)]
        [MinLength(LengthConstants.Length2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LengthConstants.Length255)]
        [MinLength(LengthConstants.Length2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(LengthConstants.Length100)]
        public string Email { get; set; }
    }
}
