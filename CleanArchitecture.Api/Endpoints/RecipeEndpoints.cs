using System.ComponentModel;
using CleanArchitecture.Application.Recipes.Commands;
using CleanArchitecture.Application.Recipes.Queries;
using Microsoft.AspNetCore.RateLimiting;

namespace CleanArchitecture.Api.Endpoints;

public sealed class RecipeEndpoints() : EndpointGroup("recipes")
{
    [HttpGet]
    public static async Task<IResult> GetRecipes(
        [FromServices] ISender mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetAllRecipesQuery("", 1, 10)));
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
        // [FromHeader(Name = "X-Idempotency-Key"), Description("Client generated idempontency key")] string requestId,
        [FromBody] CreateRecipeCommand command)
    {
        // if (!Guid.TryParse(requestId, out var parsedRequestId))
        // {
        //     return TypedResults.BadRequest();
        // }

        await mediator.Send(command with {RequestId = Guid.NewGuid()});
        
        return TypedResults.Created();
    }
    // servers:
    // - url: https://petstore.swagger.io/v1

    [EnableRateLimiting("recipes")]
    [HttpPut("{id:int}")]
    public static async Task<IResult> UpdateRecipe(
        [FromServices] ISender mediator,
        [FromRoute, Description("Id of receipe you want to update")] int id,
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