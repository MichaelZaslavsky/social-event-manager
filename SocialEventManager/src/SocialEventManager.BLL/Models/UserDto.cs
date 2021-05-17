using System;

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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
