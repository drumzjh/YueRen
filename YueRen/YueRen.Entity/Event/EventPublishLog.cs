using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Event
{
    /// <summary>
    /// 发布日志事件
    /// </summary>
    public class EventPublishLog:BaseEventInfo
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }
    }
}
