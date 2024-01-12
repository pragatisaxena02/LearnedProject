
using MassTransit;
using MassTransit.SagaStateMachine;
using Notify.Events;
namespace Notify.Services
{
    public class Notify : INotify
    {
        private readonly IBus _bus;

        public Notify(IBus bus)
        {           
            _bus = bus;
        }
        public async Task SendNotification()
        {
          var value = new NotifyTriggerEvent { Show = true };
          await _bus.Publish(value);           
        }
    }
}