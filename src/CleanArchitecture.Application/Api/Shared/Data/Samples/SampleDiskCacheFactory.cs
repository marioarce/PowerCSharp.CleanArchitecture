using System.Diagnostics;
using CleanArchitecture.Application.Api.Samples.Responses;
using CleanArchitecture.Application.Api.Shared.Factories;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Api.Shared.Data.Samples;

/// <summary>
/// Builds/fetches the disk cache sample value. On a cache miss it simulates expensive work, stores the
/// result via <c>IDiskCacheService</c>, and reports timing so the cache effect is observable.
/// </summary>
public sealed class SampleDiskCacheFactory : BaseFactory<SampleDiskCacheFactoryContext>
{
    private static readonly TimeSpan SimulatedWork = TimeSpan.FromMilliseconds(750);

    public SampleDiskCacheFactory(SampleDiskCacheFactoryContext context)
        : base(context)
    {
    }

    /// <summary>Returns the cached value for <paramref name="key"/>, building it on a miss.</summary>
    public async Task<GetSampleDiskCacheResponse> GetOrCreateAsync(string key, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        var cached = await Context.DiskCache.GetAsync<string>(key, cancellationToken).ConfigureAwait(false);
        if (cached != null)
        {
            stopwatch.Stop();
            Context.Logger.LogInformation("Disk cache HIT for sample key {Key}", key);

            return new GetSampleDiskCacheResponse
            {
                Key = key,
                Value = cached,
                CacheHit = true,
                ElapsedMs = stopwatch.ElapsedMilliseconds,
            };
        }

        Context.Logger.LogInformation("Disk cache MISS for sample key {Key}; building value", key);
        await Task.Delay(SimulatedWork, cancellationToken).ConfigureAwait(false);

        var value = $"disk-cache-sample-for:{key}@{DateTimeOffset.UtcNow:O}";
        await Context.DiskCache.SetAsync(key, value, cancellationToken).ConfigureAwait(false);

        stopwatch.Stop();
        return new GetSampleDiskCacheResponse
        {
            Key = key,
            Value = value,
            CacheHit = false,
            ElapsedMs = stopwatch.ElapsedMilliseconds,
        };
    }

    /// <summary>Returns a snapshot of the disk cache status (limited information).</summary>
    /// <remarks>
    /// Note: Disk cache doesn't expose GetKeys() method for security/performance reasons.
    /// This method provides basic status information.
    /// </remarks>
    public Task<GetSampleDiskCacheStatusResponse> GetStatusAsync(CancellationToken cancellationToken)
    {
        // For disk cache, we can't get the actual keys like with in-memory cache
        // So we'll return a basic status response
        Context.Logger.LogInformation("Disk cache status requested - key enumeration not available for disk cache");
        
        var response = new GetSampleDiskCacheStatusResponse
        {
            Count = -1, // -1 indicates unknown count for disk cache
            Keys = Array.Empty<string>(),
        };
        
        return Task.FromResult(response);
    }

    /// <summary>Simulates clearing the disk cache (limited functionality).</summary>
    /// <remarks>
    /// Note: Disk cache doesn't expose Clear() or PurgeExpired() methods through the interface for safety reasons.
    /// This method provides a placeholder implementation that logs the action.
    /// </remarks>
    public Task<int> ClearAsync(CancellationToken cancellationToken)
    {
        // For disk cache, we can't clear entries through the interface for safety reasons
        // This is a limitation of the disk cache design compared to in-memory cache
        Context.Logger.LogInformation("Disk cache clear requested - not available through IDiskCacheService interface");
        
        return Task.FromResult(0); // Return 0 since no actual clearing occurred
    }
}
