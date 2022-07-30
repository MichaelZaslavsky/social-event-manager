namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal abstract class StorageBase<TStorage, TEntity>
    where TStorage : class
    where TEntity : class
{
    private static readonly Lazy<TStorage> Lazy = new(() => Activator.CreateInstance<TStorage>());

    public static TStorage Instance
    {
        get
        {
            return Lazy.Value;
        }
    }

    public List<TEntity> Data { get; set; } = new();

    public abstract void Init();
}
