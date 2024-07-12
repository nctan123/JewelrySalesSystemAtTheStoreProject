namespace JSSATSProject.Repository.CacheManagers;

public class DiamondPriceCacheManager
{
    private readonly
        Dictionary<(int cutId, int clarityId, int colorId, int caratId, int originId, DateTime effectiveDate), decimal> _cache = new();

    public bool TryGetValue(
        (int cutId, int clarityId, int colorId, int caratId, int originId, DateTime effectiveDate) key,
        out decimal value)
    {
        var closestEffectiveDate = GetLastedEffectiveDateOf4CKey(key.cutId, key.clarityId, key.colorId, key.caratId,
            key.originId, key.effectiveDate);
        if (closestEffectiveDate.HasValue)
            key.effectiveDate = closestEffectiveDate.Value;
        return _cache.TryGetValue(key, out value);
    }

    public void SetValue(
        (int cutId, int clarityId, int colorId, int caratId, int originId, DateTime effectiveDate) key,
        decimal value)
    {
        var closestEffectiveDate = GetLastedEffectiveDateOf4CKey(key.cutId, key.clarityId, key.colorId, key.caratId,
            key.originId, key.effectiveDate);
        if (closestEffectiveDate.HasValue)
            key.effectiveDate = closestEffectiveDate.Value;
        _cache[key] = value;
    }

    public void Clear()
    {
        _cache.Clear();
    }

    public DateTime? GetLastedEffectiveDateOf4CKey(int cutId, int clarityId, int colorId, int caratId, int originId,
        DateTime timeStamp) 
    {
        if (_cache.Count == 0) return null;
        DateTime result = _cache.Keys
            .Where(c =>
                c.clarityId == clarityId
                && c.caratId == caratId
                && c.colorId == colorId
                && c.cutId == cutId
                && c.originId == originId
                && c.effectiveDate <= timeStamp
            )
            .OrderByDescending(c => c.effectiveDate)
            .Select(c => c.effectiveDate)
            .FirstOrDefault();
        return result;
    }
}