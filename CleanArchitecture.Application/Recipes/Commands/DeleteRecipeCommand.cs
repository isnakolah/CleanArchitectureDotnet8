using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Commands;

[RecipeUpdateFeature, RecipeDeleteFeature]
public sealed record DeleteRecipeCommand(int Id) 
    : IRequest<RecipeVm>;

public sealed class DeleteRecipeCommandHandler(
        IApplicationDbContext context, 
        IMapper mapper) 
    : IRequestHandler<DeleteRecipeCommand, RecipeVm>
{
    public async Task<RecipeVm> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Recipes.FindByIdAsync(request.Id, cancellationToken);

        context.Recipes.Remove(entity!);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeVm>(entity);
    }
}

public sealed class DeleteRecipeCommandValidator : AbstractValidator<DeleteRecipeCommand>
{
    public DeleteRecipeCommandValidator(IApplicationDbContext context)
    {
        RuleFor(command => command.Id)
            .MustExistAsync(recipeId => context.Recipes.ExistsAsync(recipeId))
            .WithMessage("Recipe with id {PropertyValue} does not exist");
    }
}