using System.Reflection;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Infrastructure.Loggers;

public class ScopeInformation : IScopeInformation
{
    public ScopeInformation()
    {
        HostScopeInfo = new Dictionary<string, string>
            {
                { GlobalConstants.MachineName, Environment.MachineName },
                { GlobalConstants.EntryPoint, Assembly.GetEntryAssembly().GetName().Name },
            };
    }

    public IDictionary<string, string> HostScopeInfo { get; }
}
