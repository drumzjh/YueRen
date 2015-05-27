using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity
{
    /// <summary>
    /// 访问记录
    /// </summary>
    public class VisitLog
    {
        /// <summary>
        /// 访问ID
        /// </summary>
        public int VisitID { get; set; }

        /// <summary>
        /// 访问者ID
        /// </summary>
        public long VisitUserID { get; set; }

        /// <summary>
        /// 被访问者ID
        /// </summary>
        public long VisitedUserID { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime VisitTime { get; set; }
    }
}
