using System.Data;

namespace SocialEventManager.DLL.Infrastructure
{
    public interface IDbSession
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; set; }
    }
}
