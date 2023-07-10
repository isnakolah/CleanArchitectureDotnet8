using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Queries;

public record GetRecipeByIdQuery(int Id) 
    : IRequest<RecipeVm>;

public sealed class GetRecipeByIdQueryHandler(
        IApplicationDbContext context, 
        IConfigurationProvider mapperCfg) 
    : IRequestHandler<GetRecipeByIdQuery, RecipeVm>
{
    public async Task<RecipeVm> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Recipes
            .ProjectTo<RecipeVm>(mapperCfg)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return entity!;
    }
}