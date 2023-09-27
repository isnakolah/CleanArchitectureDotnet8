using CleanArchitecture.Domain.Recipes.Entities;

namespace CleanArchitecture.Application.Recipes.Commands;

public sealed record CreateRecipeCommand(
        Guid RequestId,
        string Title,
        string Description,
        int PrepTime,
        int CookTime)
    : IdempotentCommand(RequestId);

public sealed class CreateRecipeCommandHandler(
        IApplicationDbContext context) 
    : IRequestHandler<CreateRecipeCommand>
{
    public async Task Handle(CreateRecipeCommand command, CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            Title = command.Title,
            Description = command.Description,
            PrepTime = command.PrepTime,
            CookTime = command.CookTime,
        };

        context.Recipes.Add(recipe);

        await context.SaveChangesAsync(cancellationToken);
    }
}

public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeCommandValidator()
    {
        RuleFor(x => x.Title).IsRequired();
        RuleFor(x => x.Description).IsRequired();
        RuleFor(x => x.PrepTime).GreaterThan(0).WithMessage("Prep time must be greater than 0");
        RuleFor(x => x.CookTime).GreaterThan(0).WithMessage("Cook time must be greater than 0");
    }
}
