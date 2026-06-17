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
/// Orchestrates <see cref="GetSampleDiskCacheQuery"/>: validates input, delegates the data work to
/// <see cref="SampleDiskCacheFactory"/>, and shapes the result. Stays free of HTTP concerns.
/// </summary>
public sealed class GetSampleDiskCacheQueryHandler
    : BaseRequestHandler<GetSampleDiskCacheQuery, Result<GetSampleDiskCacheResponse>>
{
    private readonly IValidator<GetSampleDiskCacheQuery> _validator;
    private readonly SampleDiskCacheFactoryContext _factoryContext;

    public GetSampleDiskCacheQueryHandler(
        ILogger<GetSampleDiskCacheQueryHandler> logger,
        IValidator<GetSampleDiskCacheQuery> validator,
        SampleDiskCacheFactoryContext factoryContext)
        : base(logger)
    {
        _validator = validator;
        _factoryContext = factoryContext;

        ArgumentNullException.ThrowIfNull(_validator);
        ArgumentNullException.ThrowIfNull(_factoryContext);
    }

    public override async Task<Result<GetSampleDiskCacheResponse>> Handle(
        GetSampleDiskCacheQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(request, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var factory = new SampleDiskCacheFactory(_factoryContext);
        var response = await factory
            .GetOrCreateAsync(request.Key, cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(response);
    }
}
