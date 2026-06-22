using Ardalis.Result;
using CleanArchitecture.Application.Api.Samples.Queries;
using CleanArchitecture.Application.Api.Samples.Responses;
using CleanArchitecture.Application.Api.Shared.Data.Samples;
using CleanArchitecture.Application.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Api.Samples.Handlers;

/// <summary>Orchestrates <see cref="GetSampleCacheStatusQuery"/> by delegating to the factory.</summary>
public sealed class GetSampleCacheStatusQueryHandler
    : BaseRequestHandler<GetSampleCacheStatusQuery, Result<GetSampleCacheStatusResponse>>
{
    /// <summary>
    /// Initializes a new instance of the GetSampleCacheStatusQueryHandler.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="factoryContext">The factory context.</param>
    public GetSampleCacheStatusQueryHandler(
        ILogger<GetSampleCacheStatusQueryHandler> logger,
        SampleCacheFactoryContext factoryContext)
        : base(logger)
    {
        _factoryContext = factoryContext;
        ArgumentNullException.ThrowIfNull(_factoryContext);
    }

    /// <summary>
    /// Handles the GetSampleCacheStatusQuery by retrieving cache status.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result with the cache status.</returns>
    public override Task<Result<GetSampleCacheStatusResponse>> Handle(
        GetSampleCacheStatusQuery request,
        CancellationToken cancellationToken)
    {
        var factory = new SampleCacheFactory(_factoryContext);
        var status = factory.GetStatus();
        return Task.FromResult(Result.Success(status));
    }

    // Private field (moved to end)
    private readonly SampleCacheFactoryContext _factoryContext;
}
