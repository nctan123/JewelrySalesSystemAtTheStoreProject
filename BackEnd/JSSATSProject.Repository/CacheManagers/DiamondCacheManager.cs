namespace JSSATSProject.Repository.CacheManagers;

using System;
using System.Collections.Generic;

public class DiamondPriceCacheManager
{
    private readonly Dictionary<(int cutId, int clarityId, int colorId, int caratId, int originId), decimal> _cache = new();

    public bool TryGetValue((int cutId, int clarityId, int colorId, int caratId, int originId) key, out decimal value)
    {
        return _cache.TryGetValue(key, out value);
    }

    public void SetValue((int cutId, int clarityId, int colorId, int caratId, int originId) key, decimal value)
    {
        _cache[key] = value;
    }

    public void Clear()
    {
        _cache.Clear();
    }
}
