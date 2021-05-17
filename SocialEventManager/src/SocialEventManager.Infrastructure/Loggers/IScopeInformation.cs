using System.Collections.Generic;

namespace SocialEventManager.Infrastructure.Loggers
{
    public interface IScopeInformation
    {
        IDictionary<string, string> HostScopeInfo { get; }
    }
}
