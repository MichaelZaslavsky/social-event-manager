using System;
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.BLL.Models
{
    // Temp class - for test purposes
    public class UserDto
    {
        public UserDto(Guid externalId, string firstName, string lastName, string email)
        {
            ExternalId = externalId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public Guid ExternalId { get; set; }

        [Required]
        [MaxLength(SupportedLengthConstants.Length255)]
        [MinLength(SupportedLengthConstants.Length2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(SupportedLengthConstants.Length255)]
        [MinLength(SupportedLengthConstants.Length2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(SupportedLengthConstants.Length100)]
        public string Email { get; set; }
    }
}
