using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Event
{
    public class EventUploadPhoto : BaseEventInfo
    {
         
        /// <summary>
        /// 照片链接
        /// </summary>
        public List<string> PhotoUrls { get; set; }
    }
}
