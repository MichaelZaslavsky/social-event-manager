using System;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DLL.Entities
{
    // Temp class - for test purposes
    [Table(TableNameConstants.Users)]
    public class User
    {
        [Computed]
        public int Id { get; set; }

        public Guid ExternalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
