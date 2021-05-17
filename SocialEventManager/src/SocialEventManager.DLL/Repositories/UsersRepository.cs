using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;

namespace SocialEventManager.DLL.Repositories
{
    // Temp class - for test purposes
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }
    }
}
