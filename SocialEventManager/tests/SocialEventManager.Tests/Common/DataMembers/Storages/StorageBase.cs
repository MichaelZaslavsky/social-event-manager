namespace SocialEventManager.Tests.Common.DataMembers.Storages;

internal abstract class StorageBase<TStorage>
{
    private static readonly Lazy<TStorage> Lazy = new(() => Activator.CreateInstance<TStorage>());

    public static TStorage Instance
    {
        get
        {
            return Lazy.Value;
        }
    }

    public abstract void Init();
}
