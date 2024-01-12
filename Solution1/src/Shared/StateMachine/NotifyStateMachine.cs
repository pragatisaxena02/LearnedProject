using MassTransit;
using Notify.Events;
using Shared.Events;

namespace Shared.StateMachine
{
    public class NotifyStateMachine : MassTransitStateMachine<NotifyState>
    {
        /// <summary>
        /// Score deletion requested state
        /// </summary>
        public State NotifyRequestedState { get; set; }
        /// <summary>
        /// The requested event
        /// </summary>

        /// <summary>
        /// Completed state
        /// </summary>
        public State NotifyCompletedState { get; set; }

        public Event<NotifyTriggerEvent> Requested { get; set; }

        /// <summary>
        /// The complete event
        /// </summary>
        public Event<NotificationCompleteEvent> NotificationComplete { get; set; }

        public NotifyStateMachine()
        {
            InstanceState(x => x.NotifiedState);

            Event(() => Requested, e => e.CorrelateById(y => y.Message.NotifyTriggerId));
            Event(() => NotificationComplete, e => e.CorrelateById(y => y.Message.NotificationId));

            // Publish the event to delete scores for the scoring group
            Initially(
                When(Requested)
                    .Then(context =>
                    {
                        var saga = context.Saga;

                        saga.CorrelationId = context.Message.NotifyTriggerId;
                        saga.Result = context.Message.Show;
                    })
                    .TransitionTo(NotifyRequestedState)
                    //.PublishAsync(context => context.Init<TrandformShowEvent>(
                    //    new TrandformShowEvent
                    //    {
                    //        Cid = Guid.NewGuid(),
                    //        Show = false
                    //    }))
                    //);

            During(NotifyRequestedState,
                When(NotificationComplete)
                    .TransitionTo(NotifyCompletedState);
                    //.PublishAsync(context => context.Init<NotificationAllDoneEvent>(
                    //    new NotificationAllDoneEvent
                    //    {
                    //        Id = Guid.NewGuid(),
                    //        IsDone = true
                    //    }))
                    //);
            // Specifies the correlation id that machine would use to identify the instance for unique Notification complete event
            //Event(() => NotificationComplete, x => x.CorrelateById(y => y.Message.NotificationId));

            // Specify the state correlationId for Notify state

            // State correlated by string value
            //InstanceState( x => x.NotifiedState );

            // State also correlated by int value
            //InstanceState( x => x.IntNotifiedState , NotifyRequestedState ); // 0 - not started , 1 - final , 2 - notified


            //Initially(
            //    When(NotificationComplete)
            //    .TransitionTo(Notified)
            //    );
        }
        // Specifify the state after an event
        public State Notified { get; set; }

        
    }
}
