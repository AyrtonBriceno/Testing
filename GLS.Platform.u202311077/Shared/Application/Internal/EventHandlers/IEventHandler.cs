using Cortex.Mediator.Notifications;
using GLS.Platform.u202311077.Shared.Domain.Model.Events;

namespace GLS.Platform.u202311077.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}