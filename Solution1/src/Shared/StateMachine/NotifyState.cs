using MassTransit;

namespace Shared.StateMachine
{
    public class NotifyState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set ; }
        public string NotifiedState { get; set ; }
        public bool Result { get; set ; }   
    }
}
