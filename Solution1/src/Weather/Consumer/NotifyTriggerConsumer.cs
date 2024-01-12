//using MassTransit;
//using Notify.Events;
//using System.Collections.Concurrent;

using MassTransit;
using Notify.Events;
using Shared.Events;

namespace Weather.Consumer
{
    public class NotifyTriggerConsumer : IConsumer<NotifyTriggerEvent>
    {
        private readonly ILogger<NotifyTriggerConsumer> _logger;

        public NotifyTriggerConsumer(ILogger<NotifyTriggerConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<NotifyTriggerEvent> context)
        {
            _logger.LogInformation(" Got notified from notification service  if allowed to show :  " + context.Message.Show);

            await context.Publish<NotificationCompleteEvent>(new 
            {
                NotificationId = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                IsShowed = true
            });            
        }
    }
}
