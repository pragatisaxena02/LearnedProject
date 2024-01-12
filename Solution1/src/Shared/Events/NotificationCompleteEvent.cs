namespace Shared.Events
{
    public class NotificationCompleteEvent
    {
       public Guid NotificationId { get; set; }
        public DateTime TimeStamp { get; set; }

        public bool IsShowed { get; set; }

    }
}
