using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class TrandformShowEvent
    {
        public Guid Cid { get; set; }
        public bool Show {  get; set; }
    }
}
