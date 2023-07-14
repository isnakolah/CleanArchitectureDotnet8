using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Queries;

public sealed record GetAllRecipesQuery(string SearchTerm, int PageIndex, int PageSize)
    : IPaginatedRequest<RecipeVm>;

public sealed class GetAllRecipesQueryHandler(
        IApplicationDbContext context,
        IConfigurationProvider mapperCfg)
    : IPaginatedRequestHandler<GetAllRecipesQuery, RecipeVm>
{
    public async Task<PaginatedResult<RecipeVm>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipesPaginatedResult = await context.Recipes
            .Where(recipe => recipe.Title.ToLower().Contains(request.SearchTerm.ToLower()))
            .ProjectTo<RecipeVm>(mapperCfg)
            .ToPaginatedResultAsync(request.PageIndex, request.PageSize, cancellationToken);

        return recipesPaginatedResult;
    }
}
