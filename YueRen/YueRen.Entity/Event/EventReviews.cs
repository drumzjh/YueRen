using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Entity.Event
{
    public class EventReviews
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public long ReviewID { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public long ReviewUserID { get; set; }

        /// <summary>
        /// 被评论者ID
        /// </summary>
        public long ReviewedUserID { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string ReviewContent { get; set; }
    }
}
