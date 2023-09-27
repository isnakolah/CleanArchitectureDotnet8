using CleanArchitecture.Application.FeatureFlags;
using Microsoft.FeatureManagement;

namespace CleanArchitecture.Infrastructure.FeatureFlags;

internal sealed class DatabaseFeatureFilter(
        IFeatureFlagService featureFlagService)
    : IFeatureFilter
{
    public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context, CancellationToken cancellationToken = new())
    {
        return await featureFlagService.IsEnabledAsync(context.FeatureFlagName, cancellationToken);
    }
}