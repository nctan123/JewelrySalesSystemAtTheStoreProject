namespace JSSATSProject.Repository.CacheManagers;

public class CacheManager<T>
{
    private readonly Dictionary<string, T> _cache = new();

    public bool TryGetValue(string key, out T value)
    {
        return _cache.TryGetValue(key, out value);
    }

    public void SetValue(string key, T value)
    {
        _cache[key] = value;
    }

    public void Clear()
    {
        _cache.Clear();
    }
}