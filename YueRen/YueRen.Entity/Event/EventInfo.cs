using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity
{
    /// <summary>
    /// 事件信息
    /// </summary>
    public class BaseEventInfo
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        public long EventID { get; set; }

        public long UserID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string UserStatus { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public EventEnum EventType { get; set; }

        /// <summary>
        /// 事件主题
        /// </summary>
        public string EventTheme{ get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccuringTime { get; set; }

        /// <summary>
        /// 赞数
        /// </summary>
        public int Great { get; set; }

        /// <summary>
        /// 转发数
        /// </summary>
        public int TransferCount { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string Content { get; set; }
    }

    public enum EventEnum
    {
        /// <summary>
        /// 分享照片
        /// </summary>
        SharePhoto,

        /// <summary>
        /// 分享相册
        /// </summary>
        SharePhotoAlbum,

        /// <summary>
        /// 转发状态
        /// </summary>
        TransferStatus,

        /// <summary>
        /// 上传照片
        /// </summary>
        UploadPhoto,

        /// <summary>
        /// 发布照片
        /// </summary>
        PublishPhoto,

        /// <summary>
        /// 发布日志
        /// </summary>
        PublishLog,

        /// <summary>
        /// 发布状态
        /// </summary>
        PublishStatus
    }
}
