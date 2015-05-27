using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Enum
{
    /// <summary>
    /// 乐队状态
    /// </summary>
    public enum BandStatusEnum
    {
        /// <summary>
        /// 活跃中
        /// </summary>
        Active,

        /// <summary>
        /// 缺主音吉它
        /// </summary>
        NeedLeadGuitar,

        /// <summary>
        /// 缺贝司
        /// </summary>
        NeedBass,

        /// <summary>
        /// 缺鼓手
        /// </summary>
        NeedDrummer,

        /// <summary>
        /// 缺节奏吉它
        /// </summary>
        NeedRythmGuitar,

        /// <summary>
        /// 缺键盘
        /// </summary>
        NeedKeyboard,

        /// <summary>
        /// 缺主唱
        /// </summary>
        NeedVocal
    }
}
