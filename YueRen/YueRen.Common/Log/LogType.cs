using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.Log
{
    public class LogType
    {
        #region 操作
        public static int OP_Login = 0;//"登录";

        public static int OP_Insert = 1;//"添加";

        public static int OP_Modify = 2;//"修改";

        public static int OP_Delete = 3;//"删除";

        public static int OP_RollBack = 4;//"回滚";

        public static int OP_AuditPass = 5;//"审核通过/发布";

        public static int OP_AuditNoPass = 6;//"审核不通过";

        public static int OP_Query = 7;//"查询";

        public static int OP_Error = 8;//"系统错误";

        public static int OP_OA_JUMP_ERROE = 9;//"oa内部错误编号";
        #endregion

        #region 状态
        public static int ST_Succeed = 0;

        public static int ST_Failed = 1;
        #endregion

        #region 等级
        /// <summary>
        /// 最低的级别
        /// 包括基本操作查询察看，用户管理，权限管理，角色管理等等
        /// </summary>
        public static int LV_Lowest = 1;

        /// <summary>
        /// 低级别
        /// 包括基本操作的添加修改删除用户管理，权限管理，角色管理等等
        /// </summary>
        public static int LV_Low = 2;

        /// <summary>
        /// 中等的级别
        /// 包括业务方面的查询，察看操作
        /// </summary>
        public static int LV_Medium = 3;

        /// <summary>
        /// 高级别
        /// 包括业务的添加操作
        /// </summary>
        public static int LV_High = 4;

        /// <summary>
        /// 最高级别
        /// 包括业务的修改删除操作
        /// </summary>
        public static int LV_Highest = 5;

        /// <summary>
        /// 特殊级别
        /// 系统异常
        /// </summary>
        public static int LV_Error = 6;
        #endregion
    }
}
