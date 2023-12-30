namespace CleanArchitecture.Application.Abstractions.EventBus;

public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}
