using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Entity.Enum;

namespace YueRen.Entity.User
{
    public class RoleInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 角色ID(所掌握或有兴趣的乐器)
        /// </summary>
        public int InstruementID { get; set; }

        /// <summary>
        /// 乐器熟练度
        /// </summary>
        public InstrumentDegreeEnum InstrumentDegree { get; set; }

    }
}
