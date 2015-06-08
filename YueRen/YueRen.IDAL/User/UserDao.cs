using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Entity;
using YueRen.Entity.User;

namespace YueRen.IDAL.User
{
    public abstract class UserDao : BaseDao
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
        public UserDao()
        {
            this.adoHelper = base.GetDefaultProvider(typeof(UserDao));
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public abstract YueRenUserEntity GetUser(int UserID);

        /// <summary>
        /// 修改用户个人信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract int UpdateUser(YueRenUserEntity user);

        /// <summary>
        /// 添加用户个人信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract int InsertUser(YueRenUserEntity user);

        /// <summary>
        /// 添加用户使用的乐器类型
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public abstract int InsertUserInstrumentType(List<int> types);

        /// <summary>
        /// 获取用户使用的乐器类型
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public abstract List<int> GetUserInstumentType(List<int> types);

        /// <summary>
        /// 获取用户Token
        /// </summary>
        /// <param name="userId"></param>
        public abstract UserToken GetUserToken(long userId);

        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public abstract bool IsUserValid(long userId, string passWord);
    }
}
