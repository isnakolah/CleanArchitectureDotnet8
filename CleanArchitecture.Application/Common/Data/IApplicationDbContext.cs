using CleanArchitecture.Domain.Recipes.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CleanArchitecture.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Recipe> Recipes { get; }

    DatabaseFacade Database { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken);
}