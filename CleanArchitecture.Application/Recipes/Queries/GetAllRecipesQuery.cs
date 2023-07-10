using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Queries;

public sealed record GetAllRecipesQuery 
    : IRequest<IEnumerable<RecipeVm>>;

public sealed class GetAllRecipesQueryHandler(
        IApplicationDbContext context, 
        IConfigurationProvider mapperCfg) 
    : IRequestHandler<GetAllRecipesQuery, IEnumerable<RecipeVm>>
{
    public async Task<IEnumerable<RecipeVm>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await context.Recipes
            .ProjectTo<RecipeVm>(mapperCfg)
            .ToArrayAsync(cancellationToken);
        
        return recipes;
    }
}