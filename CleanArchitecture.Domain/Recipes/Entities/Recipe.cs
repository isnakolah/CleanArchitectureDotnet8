namespace CleanArchitecture.Domain.Recipes.Entities;

public sealed record Recipe
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int PrepTime { get; set; }
    public required int CookTime { get; set; }
    public ICollection<RecipeIngredient> Ingredients { get; private set; } = new List<RecipeIngredient>();
    
    public void AddIngredient(Ingredient ingredient, int quantity)
    {
        var recipeIngredient = new RecipeIngredient
        {
            Ingredient = ingredient,
            Recipe = this,
            Quantity = quantity,
        };
        Ingredients.Add(recipeIngredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        var recipeIngredient = Ingredients.FirstOrDefault(x => x.Ingredient == ingredient);

        if (recipeIngredient is not null)
        {
            Ingredients.Remove(recipeIngredient);
        }
    }
}