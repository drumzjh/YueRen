using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Entity.Enum;

namespace YueRen.Entity
{
    /// <summary>
    /// 乐队信息
    /// </summary>
    public class BandInfoEntity
    {
        /// <summary>
        /// 乐队ID
        /// </summary>
        public int BandID { get; set; }

        /// <summary>
        /// 乐队名
        /// </summary>
        public string BandName { get; set; }

        /// <summary>
        /// 所属国家
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// 所属省
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// 所属市
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 乐队状态
        /// </summary>
        public BandStatusEnum BandStatus { get; set; }

        /// <summary>
        /// 乐队介绍
        /// </summary>
        public string BandIntro { get; set; }

        /// <summary>
        /// 成员信息
        /// </summary>
        public List<MemberInfoEntity> listMemberInfo { get; set; }

        /// <summary>
        /// 音乐风格
        /// </summary>
        public List<int> MusicStyle { get; set; }
    }

   
}
