using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Entity.Enum;

namespace YueRen.Entity
{
    /// <summary>
    /// 乐器
    /// </summary>
    public class UserInstrument
    {
        /// <summary>
        /// 用户乐器ID
        /// </summary>
        public long UserInstrumentID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 乐器类型
        /// </summary>
        public int InstrumentType { get; set; }

        /// <summary>
        /// 乐器名称
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// 乐器掌握程序
        /// </summary>
        public InstrumentDegreeEnum MasterDegree { get; set; }
    }
}
