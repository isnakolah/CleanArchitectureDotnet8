using CleanArchitecture.Application.Abstractions.Idempotency;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Idempotency;

internal sealed class IdempotencyService(
        ApplicationDbContext context) 
    : IIdempotencyService
{
    public async Task<bool> RequestExistsAsync(Guid requestId)
    {
        return await context
            .Set<IdempotentRequest>()
            .AnyAsync(x => x.Id == requestId);
    }

    public async Task CreateRequestAsync(Guid requestId, string name)
    {
        var idempotentRequest = new IdempotentRequest
        {
            Id = requestId,
            Name = name,
            CreatedOnUtc = DateTime.UtcNow
        };

        context.Add(idempotentRequest);
        
        await context.SaveChangesAsync();
    }
}