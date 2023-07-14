using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Domain.Recipes.Entities;

namespace CleanArchitecture.Application.Recipes.DTOs;

public record RecipeIngredientVm : IViewModel<RecipeIngredient>
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RecipeIngredient, RecipeIngredientVm>()
            .ForMember(recipeIngredientVm => recipeIngredientVm.Name, opt => opt.MapFrom(recipeIngredient => recipeIngredient.Ingredient.Name))
            .ForMember(recipeIngredientVm => recipeIngredientVm.UnitOfMeasure, opt => opt.MapFrom(recipeIngredient => recipeIngredient.Ingredient.UnitOfMeasure))
            .ForMember(recipeIngredientVm => recipeIngredientVm.Description, opt => opt.MapFrom(recipeIngredient => recipeIngredient.Ingredient.Description));
    }
} 