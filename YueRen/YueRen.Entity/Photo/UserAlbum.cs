using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Photo
{
    public class UserAlbumEntity
    {
        /// <summary>
        /// 相册ID
        /// </summary>
        public long AlbumID { get; set; }

        /// <summary>
        /// 照片Url
        /// </summary>
        public List<string> PhotoUrls { get; set; }

        /// <summary>
        /// 相册名称
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// 相册所属用户名
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
