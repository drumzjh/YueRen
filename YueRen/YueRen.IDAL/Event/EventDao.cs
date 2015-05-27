using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Entity;

namespace YueRen.IDAL.Event
{
    public abstract class EventDao:BaseDao
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
        public EventDao()
        {
            this.adoHelper = base.GetDefaultProvider(typeof(EventDao));
        }

        /// <summary>
        /// 获取好友最新事件
        /// </summary>
        /// <param name="eventCount">事件数据</param>
        /// <returns></returns>
        public abstract List<BaseEventInfo> GetLastestFriendEvents(int eventCount);

    }
}
