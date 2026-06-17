using CleanArchitecture.Application.Api.Samples.Queries;
using FluentValidation;

namespace CleanArchitecture.Application.Api.Samples.Queries;

/// <summary>
/// Validates <see cref="GetSampleDiskCacheQuery"/> input.
/// </summary>
public sealed class GetSampleDiskCacheQueryValidator : AbstractValidator<GetSampleDiskCacheQuery>
{
    public GetSampleDiskCacheQueryValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty()
            .WithMessage("Cache key is required.")
            .MaximumLength(200)
            .WithMessage("Cache key cannot exceed 200 characters.")
            .Matches(@"^[a-zA-Z0-9\-_\.:]+$")
            .WithMessage("Cache key can only contain letters, numbers, hyphens, underscores, periods, and colons.");
    }
}
