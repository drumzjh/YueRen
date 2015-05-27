using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity
{
    /// <summary>
    /// 乐队成员信息
    /// </summary>
    public class MemberInfoEntity
    {
        /// <summary>
        /// 成员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 乐队ID
        /// </summary>
        public int BandID { get; set; }

        /// <summary>
        /// 成员姓名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 成员位置
        /// </summary>
        public int MemberPositionID { get; set; }

        /// <summary>
        /// 成员简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 是否是乐人用户
        /// </summary>
        public bool IsYueRenUser { get; set; }
    }

}
