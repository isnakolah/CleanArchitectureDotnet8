namespace CleanArchitecture.Domain.Abstractions.Entities;

public abstract record BaseEntity
{
    public int Id { get; private set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}