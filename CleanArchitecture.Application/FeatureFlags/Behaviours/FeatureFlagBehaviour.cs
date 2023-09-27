using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace CleanArchitecture.Application.FeatureFlags;

internal sealed class FeatureFlagBehaviour<TRequest, TResponse>(
        ILogger<FeatureFlagBehaviour<TRequest, TResponse>> logger,
        IFeatureManager featureManager)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var isFeatureEnabled = await featureManager.IsEnabledAsync(typeof(TRequest).Name, cancellationToken);
        
        if (!isFeatureEnabled)
        {
            logger.LogWarning("Feature {FeatureName} is disabled", typeof(TRequest).Name);
            
            return default!;
        }

        var response = await next();

        return response;
    }
}