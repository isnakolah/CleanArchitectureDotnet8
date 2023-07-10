using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Recipes.DTOs;
using CleanArchitecture.Domain.Recipes.Entities;

namespace CleanArchitecture.Application.Recipes.Commands;

public sealed record CreateRecipeCommand(
        string Title,
        string Description,
        int PrepTime,
        int CookTime)
    : IRequest<Result<RecipeVm>>;

public sealed class CreateRecipeCommandHandler(
        IApplicationDbContext context,
        IMapper mapper) 
    : IRequestHandler<CreateRecipeCommand, Result<RecipeVm>>
{
    public async Task<Result<RecipeVm>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Recipe
        {
            Title = request.Title,
            Description = request.Description,
            PrepTime = request.PrepTime,
            CookTime = request.CookTime,
        };

        context.Recipes.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RecipeVm>(entity);
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
