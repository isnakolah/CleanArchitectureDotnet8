namespace CleanArchitecture.Domain.Recipes.Entities;

public record RecipeIngredient
{
    public int Id { get; set; }
    public required int Quantity { get; set; }
    public required Recipe Recipe { get; set; }
    public required Ingredient Ingredient { get; set; }
}