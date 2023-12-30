using System.Text.Json.Serialization;

namespace CleanArchitecture.Domain.EventBus.Models;

public record IntegrationEvent
{
    [JsonPropertyName("id")]
    public Guid Id { get; private init; }

    [JsonPropertyName("createDate")]
    public DateTime CreationDate { get; private init; }
}
