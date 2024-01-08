using CleanArchitecture.Domain.Abstractions.Entities;

namespace CleanArchitecture.Domain.Recipes.Entities;

public sealed record RecipeIngredient : BaseEntity
{
    public required int Quantity { get; set; }
    public required Recipe Recipe { get; set; }
    public required Ingredient Ingredient { get; set; }
}