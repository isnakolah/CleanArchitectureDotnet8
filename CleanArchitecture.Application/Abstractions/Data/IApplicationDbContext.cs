using CleanArchitecture.Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CleanArchitecture.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Recipe> Recipes { get; }
    DbSet<Ingredient> Ingredients { get; }
    DbSet<RecipeIngredient> RecipeIngredients { get; }

    DatabaseFacade Database { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken);
}