using System.Reflection;
using CleanArchitecture.Application.FeatureFlags;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.FeatureFlags;

public static class SeedDatabaseWithFeatureFlags
{
    public static IApplicationBuilder SeedFeatureFlags(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Seed(dbContext);

        return app;
    }
    
    private static void Seed(ApplicationDbContext dbContext)
    {
        var existingFeatureFlags = dbContext.FeatureFlags.ToList();

        var featureNames = GetFeatureNames();

        var newFeatureNames = featureNames
            .Where(featureName => existingFeatureFlags.All(featureFlag => featureFlag.FeatureName != featureName));

        dbContext.FeatureFlags.AddRange(newFeatureNames.Select(FeatureFlag.Create));

        dbContext.SaveChanges();
    }
    
    private static IEnumerable<string> GetFeatureNames()
    {
        var feat = typeof(Features).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Select(property => property.GetValue(null))
            .OfType<Feature>()
            .SelectMany(feature => feature.SubFeatures)
            .Select(subFeature => subFeature.Name)
            .Where(subFeatureName => !string.IsNullOrWhiteSpace(subFeatureName));

        var featureNames = typeof(Features)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(field => field is {IsLiteral: true, IsInitOnly: false})
            .Select(field => field.GetRawConstantValue() as string)
            .Where(value => !string.IsNullOrWhiteSpace(value));

        return featureNames!;
    }
}