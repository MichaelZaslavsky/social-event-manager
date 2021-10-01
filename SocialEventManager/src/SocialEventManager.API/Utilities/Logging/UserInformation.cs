using System.Collections.Generic;

namespace SocialEventManager.API.Utilities.Logging
{
    public record UserInformation
    {
        public string UserId { get; init; }

        public string UserName { get; init; }

        public IDictionary<string, IList<string>> UserClaims { get; init; }
    }
}
