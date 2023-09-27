namespace CleanArchitecture.Application.FeatureFlags;

public sealed record ToggleFeatureFlagCommand(
        Guid Id,
        bool Enabled) 
    : IRequest;
    
public sealed class ToggleFeatureFlagCommandHandler(
        IFeatureFlagService featureFlagService)
    : IRequestHandler<ToggleFeatureFlagCommand>
{
    public async Task Handle(ToggleFeatureFlagCommand request, CancellationToken cancellationToken)
    {
        await featureFlagService.ToggleFeatureFlagAsync(request.Id, request.Enabled, cancellationToken);
    }
} 