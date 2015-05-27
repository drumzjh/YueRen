using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.User
{
    /// <summary>
    /// 工作信息
    /// </summary>
    public class WorkInsertInfo
    {
        /// <summary>
        /// 添加工作信息ID
        /// </summary>
        public int WorkInsertID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
    }
}
