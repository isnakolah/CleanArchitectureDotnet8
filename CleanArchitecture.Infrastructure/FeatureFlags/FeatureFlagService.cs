using CleanArchitecture.Application.FeatureFlags;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.FeatureManagement;

namespace CleanArchitecture.Infrastructure.FeatureFlags;

internal sealed class FeatureFlagService(
        ApplicationDbContext dbContext,
        IMemoryCache cache) 
    : IFeatureFlagService
{
    private const string FeatureFlagsCacheKey = "featureFlags";

    public async Task ToggleFeatureFlagAsync(Guid id, bool enabled, CancellationToken cancellationToken)
    {
        var featureFlag = await dbContext.FeatureFlags.FindAsync(new object[] { id }, cancellationToken);
        
        if (featureFlag is null)
        {
            throw new ArgumentException($"Feature flag with id {id} does not exist.", nameof(id));
        }

        if (enabled)
        {
            featureFlag.Enable();
        }
        else
        {
            featureFlag.Disable();
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        cache.Remove(FeatureFlagsCacheKey);

        await GetFeatureFlagsAsync(cancellationToken);
    }

    public async Task<IEnumerable<FeatureFlagVm>> GetFeatureFlagsAsync(CancellationToken cancellationToken)
    {
        var featureFlags = await cache.GetOrCreateAsync(FeatureFlagsCacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromHours(5);
            
            return await dbContext.FeatureFlags
                .Select(x => new FeatureFlagVm
                {
                    Id = x.Id,
                    FeatureName = x.FeatureName,
                    IsEnabled = x.IsEnabled
                })
                .ToArrayAsync(cancellationToken);;
        });
        
        return featureFlags ?? Enumerable.Empty<FeatureFlagVm>();
    }

    public async Task<bool> IsEnabledAsync(string featureName, CancellationToken cancellationToken)
    {
        var featureFlags = await GetFeatureFlagsAsync(cancellationToken);
        
        return featureFlags.Any(featureFlagVm => featureFlagVm.FeatureName == featureName && featureFlagVm.IsEnabled);
    }
}