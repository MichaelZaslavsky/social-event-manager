namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal abstract class ListStorage<TStorage, TEntity> : StorageBase<TStorage>
    where TStorage : class
    where TEntity : class
{
    public List<TEntity> Data { get; set; } = new();
}
