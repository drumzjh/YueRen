using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.User
{
    public class MusicStylePreference
    {
        /// <summary>
        /// 音乐偏好ID
        /// </summary>
        public long MusicStylePreferenceID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 音乐风格
        /// </summary>
        public int MusicStyle { get; set; }
    }
}
