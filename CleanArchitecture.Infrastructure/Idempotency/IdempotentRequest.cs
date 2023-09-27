namespace CleanArchitecture.Infrastructure.Idempotency;

internal sealed class IdempotentRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}