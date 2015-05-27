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
    /// 音乐偏好数据抽象类
    /// </summary>
    public abstract class MusicPreferenceDao:BaseDao
    {
        public override string ConnectionString
        {
            get { return YueRenConfig.Instance.AppSetting.ConnectionString; }
        }

        public MusicPreferenceDao()
        {
            this.adoHelper = GetDefaultProvider(typeof(MusicPreferenceDao));
        }

        /// <summary>
        /// 设置个人音乐偏好
        /// </summary>
        /// <param name="preferences">音乐偏好</param>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public abstract int SetUserMusicPreference(List<int> preferences, YueRenUserEntity user);

        /// <summary>
        /// 设置乐队音乐偏好
        /// </summary>
        /// <param name="preferences"></param>
        /// <param name="band"></param>
        /// <returns></returns>
        public abstract int SetBandMusicPreference(List<int> preferences, BandInfoEntity band);

        /// <summary>
        /// 删除用户音乐偏好
        /// </summary>
        /// <param name="preferences"></param>
        /// <param name="band"></param>
        /// <returns></returns>
        public abstract int DeleteUserMusicPreference(List<int> preferences, YueRenUserEntity band);

        /// <summary>
        /// 删除乐队音乐偏好
        /// </summary>
        /// <param name="preferences"></param>
        /// <param name="band"></param>
        /// <returns></returns>
        public abstract int DeleteUserMisicPreference(List<int> preferences, YueRenUserEntity band);
    }
}
