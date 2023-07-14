using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Domain.Recipes.Entities;

namespace CleanArchitecture.Application.Recipes.DTOs;

public record RecipeVm : IViewModel<Recipe>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int TotalTime { get; set; }
    public IEnumerable<RecipeIngredientVm> Ingredients { get; set; } = Enumerable.Empty<RecipeIngredientVm>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Recipe, RecipeVm>()
            .ForMember(recipeVm => recipeVm.TotalTime, opt => opt.MapFrom(recipe => recipe.PrepTime + recipe.CookTime));
    }
}