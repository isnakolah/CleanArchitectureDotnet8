namespace CleanArchitecture.Application.FeatureFlags;

public interface IFeatureFlagService
{
    Task ToggleFeatureFlagAsync(Guid id, bool enabled, CancellationToken cancellationToken);
    Task<IEnumerable<FeatureFlagVm>> GetFeatureFlagsAsync(CancellationToken cancellationToken);
    Task<bool> IsEnabledAsync(string featureName, CancellationToken cancellationToken);
}