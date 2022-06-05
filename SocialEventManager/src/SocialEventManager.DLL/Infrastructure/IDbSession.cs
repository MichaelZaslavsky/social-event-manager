using System.Data;

namespace SocialEventManager.DAL.Infrastructure;

public interface IDbSession
{
    IDbConnection Connection { get; }

    IDbTransaction Transaction { get; set; }
}
