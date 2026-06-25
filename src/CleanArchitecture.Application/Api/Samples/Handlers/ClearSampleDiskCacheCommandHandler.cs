using Ardalis.Result;
using CleanArchitecture.Application.Api.Samples.Commands;
using CleanArchitecture.Application.Api.Shared.Data.Samples;
using CleanArchitecture.Application.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Api.Samples.Handlers;

/// <summary>
/// Orchestrates <see cref="ClearSampleDiskCacheCommand"/>: delegates the data work to
/// <see cref="SampleDiskCacheFactory"/> and shapes the result. Stays free of HTTP concerns.
/// </summary>
public sealed class ClearSampleDiskCacheCommandHandler
    : BaseRequestHandler<ClearSampleDiskCacheCommand, Result<int>>
{
    /// <summary>
    /// Initializes a new instance of the ClearSampleDiskCacheCommandHandler.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="factoryContext">The factory context.</param>
    public ClearSampleDiskCacheCommandHandler(
        ILogger<ClearSampleDiskCacheCommandHandler> logger,
        SampleDiskCacheFactoryContext factoryContext)
        : base(logger)
    {
        _factoryContext = factoryContext;
        ArgumentNullException.ThrowIfNull(_factoryContext);
    }

    /// <summary>
    /// Handles the ClearSampleDiskCacheCommand by clearing the disk cache.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result with the number of cleared keys.</returns>
    public override async Task<Result<int>> Handle(
        ClearSampleDiskCacheCommand request,
        CancellationToken cancellationToken)
    {
        var factory = new SampleDiskCacheFactory(_factoryContext);
        var clearedKeys = await factory
            .ClearAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(clearedKeys);
    }

    // Private field (moved to end)
    private readonly SampleDiskCacheFactoryContext _factoryContext;
}
