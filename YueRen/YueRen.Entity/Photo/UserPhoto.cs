using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Photo
{
    /// <summary>
    /// 图片上传
    /// </summary>
    public class UserPhotoEntity
    {
        /// <summary>
        /// 上传ID
        /// </summary>
        public long UploadID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 图片尺寸
        /// </summary>
        public ImageSize ImageSize { get; set; }

        /// <summary>
        /// 上传Ip
        /// </summary>
        public string UserIp { get; set; }

        /// <summary>
        /// 相册ID
        /// </summary>
        public long AlbumID { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
