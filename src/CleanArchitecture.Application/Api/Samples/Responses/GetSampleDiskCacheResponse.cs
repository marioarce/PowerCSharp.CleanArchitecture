namespace CleanArchitecture.Application.Api.Samples.Responses;

/// <summary>
/// Response for <see cref="GetSampleDiskCacheQuery"/> containing the cached value
/// and timing information to demonstrate cache hit/miss behavior.
/// </summary>
public sealed class GetSampleDiskCacheResponse
{
    /// <summary>The cache key that was requested.</summary>
    public string Key { get; init; } = string.Empty;

    /// <summary>The cached value (or newly generated value on miss).</summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>True if the value was retrieved from cache (hit), false if generated (miss).</summary>
    public bool CacheHit { get; init; }

    /// <summary>Time taken in milliseconds for the operation.</summary>
    public long ElapsedMs { get; init; }
}
