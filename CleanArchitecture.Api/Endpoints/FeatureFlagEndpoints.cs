 using CleanArchitecture.Application.FeatureFlags;

namespace CleanArchitecture.Api.Endpoints;

public class FeatureFlagEndpoints() : V1EndpointGroup("feature-flags")
{
    [HttpPost("{featureFlagId:guid}/enable")]
    public static async Task<IResult> EnableFeatureFlag(
        [FromServices] ISender mediator,
        [FromRoute] Guid featureFlagId)
    {
        await mediator.Send(new ToggleFeatureFlagCommand(featureFlagId, true));

        return TypedResults.Ok();
    }
    
    [HttpPost("{featureFlagId:guid}/disable")]
    public static async Task<IResult> DisableFeatureFlag(
        [FromServices] ISender mediator,
        [FromRoute] Guid featureFlagId)
    {
        await mediator.Send(new ToggleFeatureFlagCommand(featureFlagId, false));

        return TypedResults.Ok();
    }
    
    [HttpGet]
    public static async Task<IResult> GetFeatureFlags(
        [FromServices] ISender mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetFeatureFlagsQuery()));
    }
}