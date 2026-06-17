using Ardalis.Result;
using MediatR;

namespace CleanArchitecture.Application.Api.Samples.Commands;

/// <summary>
/// Command that clears the sample disk cache, demonstrating
/// PowerCSharp.Feature.Cache.Disk via <c>IDiskCacheService</c>.
/// </summary>
public sealed class ClearSampleDiskCacheCommand : IRequest<Result<int>>
{
}
