using Ardalis.Result;
using CleanArchitecture.Application.Api.Samples.Responses;
using MediatR;

namespace CleanArchitecture.Application.Api.Samples.Queries;

/// <summary>
/// Query that returns the current status of the sample disk cache,
/// demonstrating PowerCSharp.Feature.Cache.Disk via <c>IDiskCacheService</c>.
/// </summary>
public sealed class GetSampleDiskCacheStatusQuery : IRequest<Result<GetSampleDiskCacheStatusResponse>>
{
}
