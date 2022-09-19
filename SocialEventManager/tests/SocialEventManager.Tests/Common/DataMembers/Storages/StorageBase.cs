namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal abstract class StorageBase<TStorage>
{
    private static readonly Lazy<TStorage> Lazy = new(() => Activator.CreateInstance<TStorage>());

    public static TStorage Instance => Lazy.Value;

    public abstract void Init();
}
