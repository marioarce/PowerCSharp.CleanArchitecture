using Ardalis.Result;
using CleanArchitecture.Application.Api.Samples.Commands;
using CleanArchitecture.Application.Api.Shared.Data.Samples;
using CleanArchitecture.Application.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Api.Samples.Handlers;

/// <summary>Orchestrates <see cref="ClearSampleCacheCommand"/> by delegating the clear to the factory.</summary>
public sealed class ClearSampleCacheCommandHandler
    : BaseRequestHandler<ClearSampleCacheCommand, Result>
{
    /// <summary>
    /// Initializes a new instance of the ClearSampleCacheCommandHandler.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="factoryContext">The factory context.</param>
    public ClearSampleCacheCommandHandler(
        ILogger<ClearSampleCacheCommandHandler> logger,
        SampleCacheFactoryContext factoryContext)
        : base(logger)
    {
        _factoryContext = factoryContext;
        ArgumentNullException.ThrowIfNull(_factoryContext);
    }

    /// <summary>
    /// Handles the ClearSampleCacheCommand by clearing the cache.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A NoContent result.</returns>
    public override Task<Result> Handle(ClearSampleCacheCommand request, CancellationToken cancellationToken)
    {
        var factory = new SampleCacheFactory(_factoryContext);
        factory.Clear();
        return Task.FromResult(Result.NoContent());
    }

    // Private field (moved to end)
    private readonly SampleCacheFactoryContext _factoryContext;
}
