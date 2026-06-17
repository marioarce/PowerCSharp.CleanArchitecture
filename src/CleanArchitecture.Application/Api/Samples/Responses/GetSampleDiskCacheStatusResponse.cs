namespace CleanArchitecture.Application.Api.Samples.Responses;

/// <summary>
/// Response for <see cref="GetSampleDiskCacheStatusQuery"/> containing
/// the current state of the sample disk cache.
/// </summary>
public sealed class GetSampleDiskCacheStatusResponse
{
    /// <summary>The number of keys currently stored in the disk cache.</summary>
    public int Count { get; init; }

    /// <summary>The cache keys currently stored in the disk cache.</summary>
    public IReadOnlyList<string> Keys { get; init; } = Array.Empty<string>();
}
