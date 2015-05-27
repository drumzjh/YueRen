using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Event
{
    public class EventTransfer:BaseEventInfo
    {
        /// <summary>
        /// 事件来源
        /// </summary>
        public int EventSource { get; set; }
    }
}
