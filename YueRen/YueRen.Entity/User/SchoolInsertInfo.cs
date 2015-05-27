using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Entity.Enum;

namespace YueRen.Entity.User
{
    public class SchoolInsertInfo
    {
        /// <summary>
        /// 添加记录ID
        /// </summary>
        public int SchoolInsertID { get; set; }

        /// <summary>
        /// 学校类型
        /// </summary>
        public SchoolTypeEnum SchoolType { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 所学专业
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduateTime { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
