using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Entity;

namespace YueRen.IDAL.Band
{
    /// <summary>
    /// 乐队信息抽象类
    /// </summary>
    public abstract class BandInfoDao : BaseDao
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
        public BandInfoDao()
        {
            this.adoHelper = base.GetDefaultProvider(typeof(BandInfoDao));
        }

        /// <summary>
        /// 插入乐队信息
        /// </summary>
        /// <param name="bandInfo"></param>
        /// <returns></returns>
        public abstract int InsertBandInfo(BandInfoEntity bandInfo);

        /// <summary>
        /// 获取乐队信息
        /// </summary>
        /// <param name="bandId">乐队Id</param>
        /// <returns></returns>
        public abstract BandInfoEntity GetBandInfo(int bandId);

        /// <summary>
        /// 获取乐队信息
        /// </summary>
        /// <param name="bandName">乐队名</param>
        /// <returns></returns>
        public abstract BandInfoEntity GetBandInfo(string bandName);

        /// <summary>
        /// 根据乐队获取成员信息
        /// </summary>
        /// <param name="bandId">乐队Id</param>
        /// <returns></returns>
        public abstract List<MemberInfoEntity> GetBandMemberInfo(int bandId);

        /// <summary>
        /// 根据乐队Id获取已注册成员信息
        /// </summary>
        /// <param name="bandId">乐队Id</param>
        /// <returns></returns>
        public abstract List<YueRenUserEntity> GetBandMemberUserInfo(int bandId);

        /// <summary>
        /// 根据风格获取乐队信息
        /// </summary>
        /// <param name="styleId">风格Id</param>
        /// <returns></returns>
        public abstract List<BandInfoEntity> GetBandsByMusicStyle(List<int> styleId);

        /// <summary>
        /// 插入乐队成员信息
        /// </summary>
        /// <param name="memberInfo">乐队成员</param>
        /// <returns></returns>
        public abstract int InsertBandMember(MemberInfoEntity memberInfo);

        /// <summary>
        /// 删除乐队成员信息
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public abstract int DeleteBandMember(MemberInfoEntity memberInfo);

        /// <summary>
        /// 根据地区获取乐队信息
        /// </summary>
        /// <param name="countryID"></param>
        /// <param name="provinceID"></param>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public abstract List<BandInfoEntity> GetBandsByArea(int countryID, int provinceID, int cityID);
    }
}
