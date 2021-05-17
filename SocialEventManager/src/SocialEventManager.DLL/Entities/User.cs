using System;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DLL.Entities
{
    // Temp class - for test purposes
    [Table(TableNamesConstants.Users)]
    public class User
    {
        public User()
        {
        }

        public User(Guid externalId, string firstName, string lastName, string email)
        {
            ExternalId = externalId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        [Computed]
        public int Id { get; set; }

        public Guid ExternalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
