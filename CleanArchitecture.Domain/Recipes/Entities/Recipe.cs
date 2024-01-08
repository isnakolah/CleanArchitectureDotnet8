using CleanArchitecture.Domain.Abstractions.Entities;

namespace CleanArchitecture.Domain.Recipes.Entities;

public sealed record Recipe : BaseEntity
{
    private Recipe()
    {
    }

    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int PrepTime { get; private set; }
    public int CookTime { get; private set; }
    public ICollection<RecipeIngredient> Ingredients { get; private set; } = new List<RecipeIngredient>();
    
    public static Recipe Create(string title, string description, int prepTime, int cookTime)
    {
        return new Recipe
        {
            Title = title,
            Description = description,
            PrepTime = prepTime,
            CookTime = cookTime,
        };
    }
    
    public void Update(string? title = null, string? description = null, int? prepTime = null, int? cookTime = null)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        PrepTime = prepTime ?? PrepTime;
        CookTime = cookTime ?? CookTime;
    }

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