using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.DAL.Repositories
{
    // Temp class - for test purposes
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(IDbSession session)
            : base(session)
        {
        }
    }
}
