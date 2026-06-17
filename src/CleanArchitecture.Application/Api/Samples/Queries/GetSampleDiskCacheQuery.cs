using Ardalis.Result;
using CleanArchitecture.Application.Api.Samples.Responses;
using MediatR;

namespace CleanArchitecture.Application.Api.Samples.Queries;

/// <summary>
/// Query that returns a cached sample value for the given key, demonstrating
/// PowerCSharp.Feature.Cache.Disk via <c>IDiskCacheService</c>.
/// </summary>
public sealed class GetSampleDiskCacheQuery : IRequest<Result<GetSampleDiskCacheResponse>>
{
    public GetSampleDiskCacheQuery(string key)
    {
        Key = key;
    }

    /// <summary>The cache key to read or populate.</summary>
    public string Key { get; }
}
