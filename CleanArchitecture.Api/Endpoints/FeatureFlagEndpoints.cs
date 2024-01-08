using System.ComponentModel;
using CleanArchitecture.Application.FeatureFlags;

namespace CleanArchitecture.Api.Endpoints;

public sealed class FeatureFlagEndpoints() : EndpointGroup("feature-flags")
{
    [HttpPost("{featureFlagId:guid}/enable")]
    [EndpointSummary("Enables a feature flag")]
    public static async Task<IResult> EnableFeatureFlag(
        [FromServices] ISender mediator,
        [FromRoute, Description("Feature flag id you want to enable")] Guid featureFlagId)
    {
        await mediator.Send(new ToggleFeatureFlagCommand(featureFlagId, true));

        return TypedResults.Ok();
    }
    
    [HttpPost("{featureFlagId:guid}/disable")]
    [EndpointSummary("Disables a feature flag")]
    public static async Task<IResult> DisableFeatureFlag(
        [FromServices] ISender mediator,
        [FromRoute, Description("Feature flag id you want to disable")] Guid featureFlagId)
    {
        await mediator.Send(new ToggleFeatureFlagCommand(featureFlagId, false));

        return TypedResults.Ok();
    }
    
    [HttpGet]
    [EndpointSummary("Gets all feature flags")]
    public static async Task<IResult> GetFeatureFlags(
        [FromServices] ISender mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetFeatureFlagsQuery()));
    }
}