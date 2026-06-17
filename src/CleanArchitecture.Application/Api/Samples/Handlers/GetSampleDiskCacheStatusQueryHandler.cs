using Ardalis.Result;
using CleanArchitecture.Application.Api.Samples.Queries;
using CleanArchitecture.Application.Api.Samples.Responses;
using CleanArchitecture.Application.Api.Shared.Data.Samples;
using CleanArchitecture.Application.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Api.Samples.Handlers;

/// <summary>
/// Orchestrates <see cref="GetSampleDiskCacheStatusQuery"/>: delegates the data work to
/// <see cref="SampleDiskCacheFactory"/> and shapes the result. Stays free of HTTP concerns.
/// </summary>
public sealed class GetSampleDiskCacheStatusQueryHandler
    : BaseRequestHandler<GetSampleDiskCacheStatusQuery, Result<GetSampleDiskCacheStatusResponse>>
{
    private readonly SampleDiskCacheFactoryContext _factoryContext;

    public GetSampleDiskCacheStatusQueryHandler(
        ILogger<GetSampleDiskCacheStatusQueryHandler> logger,
        SampleDiskCacheFactoryContext factoryContext)
        : base(logger)
    {
        _factoryContext = factoryContext;
        ArgumentNullException.ThrowIfNull(_factoryContext);
    }

    public override async Task<Result<GetSampleDiskCacheStatusResponse>> Handle(
        GetSampleDiskCacheStatusQuery request,
        CancellationToken cancellationToken)
    {
        var factory = new SampleDiskCacheFactory(_factoryContext);
        var response = await factory
            .GetStatusAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(response);
    }
}
