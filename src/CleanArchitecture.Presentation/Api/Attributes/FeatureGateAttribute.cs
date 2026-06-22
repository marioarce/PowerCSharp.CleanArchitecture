using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PowerCSharp.Features.Abstractions;

namespace CleanArchitecture.Presentation.Api.Attributes;

/// <summary>
/// Gates a controller or action behind a PowerCSharp feature flag. When the feature is disabled,
/// the endpoint responds with 404 so its existence is hidden (e.g. in Production).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class FeatureGateAttribute : Attribute, IAsyncActionFilter
{
    /// <summary>
    /// Initializes a new instance of the FeatureGateAttribute.
    /// </summary>
    /// <param name="featureKey">The feature key to check.</param>
    public FeatureGateAttribute(string featureKey)
    {
        _featureKey = featureKey;
    }

    /// <summary>
    /// Executes the action filter to check if the feature is enabled.
    /// </summary>
    /// <param name="context">The action executing context.</param>
    /// <param name="next">The next action delegate.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var flags = context.HttpContext.RequestServices.GetService<IFeatureFlagProvider>();

        if (flags is null || !flags.IsEnabled(_featureKey))
        {
            context.Result = new NotFoundResult();
            return;
        }

        await next().ConfigureAwait(false);
    }

    // Private field (moved to end)
    private readonly string _featureKey;
}
