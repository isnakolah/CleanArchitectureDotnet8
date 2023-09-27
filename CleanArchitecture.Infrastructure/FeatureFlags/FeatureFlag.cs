namespace CleanArchitecture.Infrastructure.FeatureFlags;

internal sealed record FeatureFlag
{
    public Guid Id { get; init; }
    public required string FeatureName { get; init; }
    public bool IsEnabled { get; private set; }

    public static FeatureFlag Create(string featureName)
    {
        return new FeatureFlag
        {
            FeatureName = featureName,
            IsEnabled = false
        };
    }
    
    public void Enable()
    {
        IsEnabled = true;
    }
    
    public void Disable()
    {
        IsEnabled = false;
    }
}