namespace CleanArchitecture.Application.FeatureFlags;

public sealed record FeatureFlagVm
{
    public Guid Id { get; init; }
    public required string FeatureName { get; init; }
    public bool IsEnabled { get; init; }
}