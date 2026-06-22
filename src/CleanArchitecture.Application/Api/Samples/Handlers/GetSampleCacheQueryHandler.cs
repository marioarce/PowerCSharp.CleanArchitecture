using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CleanArchitecture.Application.Api.Samples.Queries;
using CleanArchitecture.Application.Api.Samples.Responses;
using CleanArchitecture.Application.Api.Shared.Data.Samples;
using CleanArchitecture.Application.Common.Handlers;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Api.Samples.Handlers;

/// <summary>
/// Orchestrates <see cref="GetSampleCacheQuery"/>: validates input, delegates the data work to
/// <see cref="SampleCacheFactory"/>, and shapes the result. Stays free of HTTP concerns.
/// </summary>
public sealed class GetSampleCacheQueryHandler
    : BaseRequestHandler<GetSampleCacheQuery, Result<GetSampleCacheResponse>>
{
    /// <summary>
    /// Initializes a new instance of the GetSampleCacheQueryHandler.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="validator">The validator instance.</param>
    /// <param name="factoryContext">The factory context.</param>
    public GetSampleCacheQueryHandler(
        ILogger<GetSampleCacheQueryHandler> logger,
        IValidator<GetSampleCacheQuery> validator,
        SampleCacheFactoryContext factoryContext)
        : base(logger)
    {
        _validator = validator;
        _factoryContext = factoryContext;

        ArgumentNullException.ThrowIfNull(_validator);
        ArgumentNullException.ThrowIfNull(_factoryContext);
    }

    /// <summary>
    /// Handles the GetSampleCacheQuery by validating input and retrieving cache data.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result with the cache response.</returns>
    public override async Task<Result<GetSampleCacheResponse>> Handle(
        GetSampleCacheQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(request, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var factory = new SampleCacheFactory(_factoryContext);
        var response = await factory
            .GetOrCreateAsync(request.Key, cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(response);
    }

    // Private fields (moved to end)
    private readonly IValidator<GetSampleCacheQuery> _validator;
    private readonly SampleCacheFactoryContext _factoryContext;
}
