using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Event
{
    /// <summary>
    /// 公布照片实体类
    /// </summary>
    public class EventPublishPhoto:BaseEventInfo
    {
       
        /// <summary>
        /// 照片Url
        /// </summary>
        public List<string> PhotoUrls { get; set; }
    }
}
