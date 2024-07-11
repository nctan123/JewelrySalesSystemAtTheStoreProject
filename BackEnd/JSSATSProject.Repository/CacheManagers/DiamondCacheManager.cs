namespace JSSATSProject.Repository.CacheManagers;

public class DiamondPriceCacheManager
{
    private readonly
        Dictionary<(int cutId, int clarityId, int colorId, int caratId, int originId, DateTime maxEffectiveDay),
            decimal> _cache = new();

    public bool TryGetValue(
        (int cutId, int clarityId, int colorId, int caratId, int originId, DateTime maxEffectiveDay) key,
        out decimal value)
    {
        var closestEffectiveDate = GetLastedEffectiveDate(key.cutId, key.clarityId, key.colorId, key.caratId,
            key.originId, key.maxEffectiveDay);
        if (closestEffectiveDate.HasValue)
            key.maxEffectiveDay = closestEffectiveDate.Value;
        return _cache.TryGetValue(key, out value);
    }

    public void SetValue(
        (int cutId, int clarityId, int colorId, int caratId, int originId, DateTime maxEffectiveDay) key,
        decimal value)
    {
        var closestEffectiveDate = GetLastedEffectiveDate(key.cutId, key.clarityId, key.colorId, key.caratId,
            key.originId, key.maxEffectiveDay);
        if (closestEffectiveDate.HasValue)
            key.maxEffectiveDay = closestEffectiveDate.Value;
        _cache[key] = value;
    }

    public void Clear()
    {
        _cache.Clear();
    }

    public DateTime? GetLastedEffectiveDate(int cutId, int clarityId, int colorId, int caratId, int originId,
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
                && c.maxEffectiveDay <= timeStamp
            )
            .OrderByDescending(c => c.maxEffectiveDay)
            .Select(c => c.maxEffectiveDay)
            .FirstOrDefault();
        return result;
    }
}