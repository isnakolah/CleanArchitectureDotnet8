using System.Reflection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.FeatureFlags;

internal sealed class FeatureFlagBehaviour<TRequest, TResponse>(
        ILogger<FeatureFlagBehaviour<TRequest, TResponse>> logger,
        IFeatureFlagService featureFlagService)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var featureName = typeof(TRequest).GetCustomAttribute<FeatureAttribute>()?.Name;

        if (string.IsNullOrWhiteSpace(featureName))
        {
            return await next();
        }

        var isFeatureEnabled = await featureFlagService.IsEnabledAsync(featureName, cancellationToken);

        if (isFeatureEnabled)
        {
            return await next();
        }

        logger.LogWarning("Feature {FeatureName} is disabled", typeof(TRequest).Name);
            
        throw new FeatureDisabledException(featureName);
    }
}

public sealed class FeatureDisabledException(string featureName) : Exception($"Feature {featureName} is disabled");