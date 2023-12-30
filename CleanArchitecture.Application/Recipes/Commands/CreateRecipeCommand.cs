using CleanArchitecture.Domain.Recipes.Entities;

namespace CleanArchitecture.Application.Recipes.Commands;

[Feature(Feature.Recipe)]
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
        RuleFor(command => command.Title).IsRequired();
        RuleFor(command => command.Description).IsRequired();
        RuleFor(command => command.PrepTime).GreaterThan(0).WithMessage("Prep time must be greater than 0");
        RuleFor(command => command.CookTime).GreaterThan(0).WithMessage("Cook time must be greater than 0");
    }
}
