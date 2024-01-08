using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Commands;

[RecipeUpdateFeature]
public sealed record UpdateRecipeCommand(
        int Id,
        string Title,
        string Description,
        int PrepTime,
        int CookTime)
    : IRequest<RecipeVm>;

public sealed class UpdateRecipeCommandHandler(
        IApplicationDbContext context,
        IMapper mapper)
    : IRequestHandler<UpdateRecipeCommand, RecipeVm>
{
    public async Task<RecipeVm> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes.FindByIdAsync(request.Id, cancellationToken);
        
        recipe!.Update(
            request.Title,
            request.Description,
            request.PrepTime,
            request.CookTime);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeVm>(recipe);
    }
}

public class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
{
    public UpdateRecipeCommandValidator(IApplicationDbContext context)
    {
        RuleFor(command => command.Title).IsRequired();
        When(command => string.IsNullOrWhiteSpace(command.Title), () =>
        {
            RuleFor(command => command.Title)
                .MaximumLength(2000)
                .WithMessage("`{PropertyTitle}` must not exceed 80 characters");
            RuleFor(command => command.Title)
                .MustExistAsync(title => context.Recipes.ExistsAsync(recipe => recipe.Title == title))
                .WithMessage("Recipe with title `{PropertyValue}` already exists");
        });
        RuleFor(command => command.Description).IsRequired();
        RuleFor(command => command.PrepTime).IsRequired();
        RuleFor(command => command.PrepTime)
            .LessThanOrEqualTo(0)
            .WithMessage("`{PropertyTitle}` must be greater than or equal to 0")
            .When(command => command.PrepTime < 0);
    }
}