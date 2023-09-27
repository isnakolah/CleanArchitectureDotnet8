using CleanArchitecture.Application.Recipes.Commands;
using CleanArchitecture.Application.Recipes.Queries;
using Microsoft.AspNetCore.RateLimiting;

namespace CleanArchitecture.Api.Endpoints;

public class RecipeEndpoints() : V1EndpointGroup("recipes")
{
    [HttpGet]
    public static async Task<IResult> GetRecipes(
        [FromServices] ISender mediator,
        [FromQuery] GetAllRecipesQuery query)
    {
        return TypedResults.Ok(await mediator.Send(query));
    }

    [HttpGet("{id:int}")]
    public static async Task<IResult> GetRecipe(
        [FromServices] ISender mediator,
        [FromRoute] int id)
    {
        return TypedResults.Ok(await mediator.Send(new GetRecipeByIdQuery(id)));
    }

    [HttpPost]
    public static async Task<IResult> CreateRecipe(
        [FromServices] ISender mediator,
        [FromHeader(Name = "X-Idempotency-Key")] string requestId,
        [FromBody] CreateRecipeCommand command)
    {
        if (!Guid.TryParse(requestId, out var parsedRequestId))
        {
            return TypedResults.BadRequest();
        }
        
        await mediator.Send(command with {RequestId = parsedRequestId});
        
        return TypedResults.Created();
    }

    [EnableRateLimiting("recipes")]
    [HttpPut("{id:int}")]
    public static async Task<IResult> UpdateRecipe(
        [FromServices] ISender mediator,
        [FromRoute] int id,
        [FromBody] UpdateRecipeCommand command)
    {
        return TypedResults.Ok(await mediator.Send(command with {Id = id}));
    }

    [HttpDelete("{id:int}")]
    public static async Task<IResult> DeleteRecipe(
        [FromServices] ISender mediator,
        [FromRoute] int id)
    {
        return TypedResults.Ok(await mediator.Send(new DeleteRecipeCommand(id)));
    }
}