namespace CleanArchitecture.Domain.Recipes.Entities;

public record Ingredient
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string UnitOfMeasure { get; set; }
    public ICollection<RecipeIngredient> RecipeIngredients { get; private set; } = new List<RecipeIngredient>();
}