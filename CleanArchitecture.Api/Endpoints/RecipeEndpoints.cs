using CleanArchitecture.Application.Recipes.Commands;
using CleanArchitecture.Application.Recipes.Queries;
using Microsoft.AspNetCore.RateLimiting;

namespace CleanArchitecture.Api.Endpoints;

public class RecipeEndpoints() : EndpointGroup("recipes")
{
    [HttpGet]
    public static async Task<IResult> GetRecipes(
        [FromServices] ISender mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetAllRecipesQuery()));
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
        [FromBody] CreateRecipeCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match<IResult>(
            TypedResults.Ok,
            TypedResults.BadRequest,
            TypedResults.NotFound
        );
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