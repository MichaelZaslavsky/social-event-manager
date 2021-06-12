using System;

namespace SocialEventManager.DLL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}
