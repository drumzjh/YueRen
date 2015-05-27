using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Entity.User;

namespace YueRen.IDAL.User
{
    /// <summary>
    /// 学校信息数据抽象类
    /// </summary>
    public class SchoolInsertDao:BaseDao
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public override string ConnectionString
        {
            get { return YueRenConfig.Instance.AppSetting.ConnectionString; }
        }

        /// <summary>
        /// 构造函数，实现adoHelper初始化
        /// </summary>
        public SchoolInsertDao()
        {
            this.adoHelper = base.GetDefaultProvider(typeof(SchoolInsertDao));
        }

        /// <summary>
        /// 添加学校信息
        /// </summary>
        /// <param name="schoolInsertInfo">学校信息</param>
        /// <returns></returns>
        public abstract int InsertSchoolInfo(SchoolInsertInfo schoolInsertInfo);

        /// <summary>
        /// 删除学校信息
        /// </summary>
        /// <param name="insertId"></param>
        /// <returns></returns>
        public abstract int DeleteSchoolInfo(int insertID);

        /// <summary>
        /// 获取学校信息
        /// </summary>
        /// <param name="insertId">插入记录Id</param>
        /// <returns></returns>
        public abstract SchoolInsertInfo GetSchoolInfo(int insertID);

        /// <summary>
        /// 获取用户所有学校信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public abstract List<SchoolInsertInfo> GetSchoolsInfo(long userID);

    }
}
