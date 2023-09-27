namespace CleanArchitecture.Application.FeatureFlags;

public sealed record GetFeatureFlagsQuery : IRequest<IEnumerable<FeatureFlagVm>>;

public sealed class GetFeatureFlagsQueryHandler(
        IFeatureFlagService featureFlagService) 
    : IRequestHandler<GetFeatureFlagsQuery, IEnumerable<FeatureFlagVm>>
{
    public async Task<IEnumerable<FeatureFlagVm>> Handle(GetFeatureFlagsQuery request, CancellationToken cancellationToken)
    {
        return await featureFlagService.GetFeatureFlagsAsync(cancellationToken);
    }
}