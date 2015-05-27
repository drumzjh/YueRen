using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class YueRenUserEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public int Country { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public int City { get; set; }

        /// <summary>
        /// 用户所在省
        /// </summary>
        public int Province { get; set; }

        /// <summary>
        /// 用户所在区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string Head_Image { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }


        public string Mail { get; set; }
    }
}
