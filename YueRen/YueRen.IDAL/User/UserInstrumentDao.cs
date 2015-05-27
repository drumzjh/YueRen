using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Entity;

namespace YueRen.IDAL.User
{
    /// <summary>
    /// 用户与乐器关系
    /// </summary>
    public abstract class UserInstrumentDao:BaseDao
    {

        public override string ConnectionString
        {
            get { return YueRenConfig.Instance.AppSetting.ConnectionString; }
        }

        public UserInstrumentDao()
        {
            this.adoHelper = GetDefaultProvider(typeof(UserInstrumentDao));
        }

        /// <summary>
        /// 设定用户所使用乐器
        /// </summary>
        /// <param name="instruments">所有乐器</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public int SetUserInstrument(List<UserInstrument> instruments);

        /// <summary>
        /// 删除用户与乐器关系
        /// </summary>
        /// <param name="userInstrumentID"></param>
        /// <returns></returns>
        public int DeleteInstrument(long userInstrumentID);

        /// <summary>
        /// 更改用户乐器关系(包括熟练度)
        /// </summary>
        /// <param name="userInstrument"></param>
        /// <returns></returns>
        public int ChangeUserInstrument(UserInstrument userInstrument);
    }
}
