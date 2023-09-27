namespace MediatR;

public abstract record IdempotentCommand(Guid RequestId) : IRequest;