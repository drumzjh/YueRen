using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Event
{
    /// <summary>
    /// 分享相册事件
    /// </summary>
    public class EventSharePhotoAlbum:BaseEventInfo
    {
        /// <summary>
        /// 相册Url
        /// </summary>
        public string AlbumUrls { get; set; }
    }
}
