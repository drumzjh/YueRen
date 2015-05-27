using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Event
{
    /// <summary>
    /// 享受照片事件
    /// </summary>
    public class EventSharePhoto : BaseEventInfo
    {
        /// <summary>
        /// 分享的照片
        /// </summary>
        public List<string> PhotoUrls { get; set; }
    }
}
