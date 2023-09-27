using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Commands;

[Feature(RecipeCrud)]
public sealed record DeleteRecipeCommand(int Id) 
    : IRequest<RecipeVm>;

public sealed class DeleteRecipeCommandHandler(
        IApplicationDbContext context, 
        IMapper mapper) 
    : IRequestHandler<DeleteRecipeCommand, RecipeVm>
{
    public async Task<RecipeVm> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Recipes.FindAsync(request.Id);

        context.Recipes.Remove(entity!);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeVm>(entity);
    }
}

public class DeleteRecipeCommandValidator : AbstractValidator<DeleteRecipeCommand>
{
    public DeleteRecipeCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.Id)
            .MustExistAsync(x => context.Recipes.ExistsAsync(x))
            .WithMessage("Recipe with id {PropertyValue} does not exist");
    }
}