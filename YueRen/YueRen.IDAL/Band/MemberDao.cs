using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Entity;
using YueRen.Entity.Enum;

namespace YueRen.IDAL.Band
{
    public abstract class MemberDao : BaseDao
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
        public MemberDao()
        {
            this.adoHelper = base.GetDefaultProvider(typeof(MemberDao));
        }

        /// <summary>
        /// 判断乐队成员是否是乐人用户
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public abstract bool IsYueRenUser(MemberInfoEntity memberInfo);

        /// <summary>
        /// 获取成员在乐队中所在的角色
        /// </summary>
        /// <param name="bandId">乐队Id</param>
        /// <param name="memberName">成员名称</param>
        /// <returns>乐器名</returns>
        public abstract string GetMemberRoleId(int bandId, string memberName);

        /// <summary>
        /// 获取乐队中所属角色的人名
        /// </summary>
        /// <param name="bandId">乐队Id</param>
        /// <param name="code">乐队码</param>
        /// <returns></returns>
        public abstract string GetMemberNameByInstrumentCode(int bandId, int code);

        /// <summary>
        /// 获取乐队中所有乐器
        /// </summary>
        /// <param name="bandId">乐队Id</param>
        /// <returns></returns>
        public abstract Dictionary<int, string> GetInstrumentsInBand(int bandId);

        /// <summary>
        /// 获取乐队所有成员信息
        /// </summary>
        /// <param name="bandId"></param>
        /// <returns></returns>
        public abstract List<MemberInfoEntity> GetAllMembersInBand(int bandId);

        /// <summary>
        /// 获取乐队状态信息
        /// </summary>
        /// <param name="bandId"></param>
        /// <returns></returns>
        public abstract BandStatusEnum GetBandStatus(int bandId);

        /// <summary>
        /// 根据乐队名获取乐队信息(也许会重名)
        /// </summary>
        /// <param name="bandName"></param>
        /// <returns></returns>
        public abstract List<BandInfoEntity> GetBandInfo(string bandName);

        /// <summary>
        /// 插入乐队信息
        /// </summary>
        /// <param name="bandInfo"></param>
        /// <returns></returns>
        public abstract int InsertBandInfo(BandInfoEntity bandInfo);

        /// <summary>
        /// 更新乐队信息
        /// </summary>
        /// <param name="bandInfo"></param>
        /// <returns></returns>
        public abstract int UpdateBandInfo(BandInfoEntity bandInfo);
    }
}
