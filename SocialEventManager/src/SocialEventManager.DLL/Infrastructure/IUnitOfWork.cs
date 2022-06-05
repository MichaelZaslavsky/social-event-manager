namespace SocialEventManager.DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}
