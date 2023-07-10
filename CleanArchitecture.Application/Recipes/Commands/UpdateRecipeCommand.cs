using CleanArchitecture.Application.Recipes.DTOs;

namespace CleanArchitecture.Application.Recipes.Commands;

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
        var recipe = await context.Recipes.FindAsync(request.Id);

        recipe!.Title = request.Title;
        recipe.Description = request.Description;
        recipe.PrepTime = request.PrepTime;
        recipe.CookTime = request.CookTime;

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeVm>(recipe);
    }
}

public class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
{
    public UpdateRecipeCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.Title).IsRequired();
        When(x => string.IsNullOrWhiteSpace(x.Title), () =>
        {
            RuleFor(x => x.Title)
                .MaximumLength(2000)
                .WithMessage("`{PropertyTitle}` must not exceed 80 characters");
            RuleFor(x => x.Title)
                .MustExistAsync(title => context.Recipes.ExistsAsync(x => x.Title == title))
                .WithMessage("Recipe with title `{PropertyValue}` already exists");
        });
        RuleFor(x => x.Description).IsRequired();
        RuleFor(x => x.PrepTime).IsRequired();
        RuleFor(x => x.PrepTime)
            .LessThanOrEqualTo(0)
            .WithMessage("`{PropertyTitle}` must be greater than or equal to 0")
            .When(x => x.PrepTime < 0);
    }
}