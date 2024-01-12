using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class NotificationAllDoneEvent
    {
        public Guid Id { get; set; }    
        public bool IsDone { get; set; }
    }
}
