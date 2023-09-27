using CleanArchitecture.Application.Abstractions.Idempotency;

namespace CleanArchitecture.Application.Abstractions.Behaviours;

internal sealed class IdempotentBehaviour<TRequest, TResponse>(
        IIdempotencyService idempotencyService) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IdempotentCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (await idempotencyService.RequestExistsAsync(request.RequestId))
        {
            return default!;
        }
        
        await idempotencyService.CreateRequestAsync(request.RequestId, typeof(TRequest).Name);
        
        var response = await next();
        
        return response;
    }
}