using CleanArchitecture.Domain.EventBus.Models;

namespace CleanArchitecture.Application.Abstractions.EventBus;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler;