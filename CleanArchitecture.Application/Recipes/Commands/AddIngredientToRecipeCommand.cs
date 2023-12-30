using CleanArchitecture.Application.Abstractions.Models;
using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Commands;

[Feature(Recipe)]
public sealed record AddIngredientToRecipeCommand(
        int RecipeId,
        int IngredientId,
        int Quantity)
    : IRequest<IResult<RecipeIngredientVm>>;

public sealed class AddIngredientToRecipeCommandHandler(
        IApplicationDbContext context,
        IMapper mapper)
    : IRequestHandler<AddIngredientToRecipeCommand, IResult<RecipeIngredientVm>>
{
    public async Task<IResult<RecipeIngredientVm>> Handle(AddIngredientToRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeEntity = await context.Recipes
            .Include(recipe => recipe.Ingredients)
                .ThenInclude(recipeIngredient => recipeIngredient.Ingredient)
            .FirstAsync(recipe => recipe.Id == request.RecipeId, cancellationToken);

        var ingredientEntity = await context.Ingredients
              .FirstAsync(ingredient => ingredient.Id == request.IngredientId, cancellationToken);

        recipeEntity.AddIngredient(ingredientEntity, request.Quantity);

        await context.SaveChangesAsync(cancellationToken);

        var recipeIngredientVm = mapper.Map<RecipeIngredientVm>(recipeEntity.Ingredients.Last());

        return IResult<RecipeIngredientVm>.Success(recipeIngredientVm);
    }
}

public class AddIngredientToRecipeCommandValidator
    : AbstractValidator<AddIngredientToRecipeCommand>
{
    public AddIngredientToRecipeCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.RecipeId)
            .MustExistAsync(x => context.Recipes.ExistsAsync(x)).WithMessage("Recipe with id {PropertyValue} does not exist");

        RuleFor(x => x.IngredientId)
            .MustExistAsync(x => context.Ingredients.ExistsAsync(x)).WithMessage("Ingredient with id {PropertyValue} does not exist");

        RuleFor(x => x.IngredientId)
            .MustAsync(async (ingredientId, cancellationToken) =>
            {
                var ingredientAlreadyExists = await context.RecipeIngredients
                    .AnyAsync(recipeIngredient => recipeIngredient.Ingredient.Id == ingredientId, cancellationToken);

                return !ingredientAlreadyExists;
            }).WithMessage("Ingredient with id {PropertyValue} already exists in recipe");
    }
}
