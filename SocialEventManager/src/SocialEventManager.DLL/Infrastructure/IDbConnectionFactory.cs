using System.Data;

namespace SocialEventManager.DLL.Infrastructure
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection();
    }
}
