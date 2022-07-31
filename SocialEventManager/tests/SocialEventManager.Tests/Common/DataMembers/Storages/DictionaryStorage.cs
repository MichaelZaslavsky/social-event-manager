namespace SocialEventManager.Tests.Common.DataMembers.Storages;
internal abstract class DictionaryStorage<TStorage, TKey, TValue> : StorageBase<TStorage>
    where TStorage : class
    where TKey : notnull
{
    public IDictionary<TKey, TValue> Data { get; set; } = new Dictionary<TKey, TValue>();
}
