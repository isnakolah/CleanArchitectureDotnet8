namespace CleanArchitecture.Infrastructure.Idempotency;

internal sealed class IdempotentRequest
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTime CreatedOnUtc { get; init; }
}