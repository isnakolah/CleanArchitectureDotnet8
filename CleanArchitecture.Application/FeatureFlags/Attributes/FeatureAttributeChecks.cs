using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.FeatureFlags;

internal static class FeatureAttributeChecks
{
    public static IServiceCollection AddFeatureAttributeChecks(this IServiceCollection services)
    {
        // scan for assemblies for FeatureAttribute and make sure they implement the correct interface IRequestBase
       
        return services;
        
    }
}