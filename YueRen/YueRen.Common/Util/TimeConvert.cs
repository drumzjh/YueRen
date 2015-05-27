using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.Util
{
    /// <summary>
    /// 获取时间字符串
    /// </summary>
    public class TimeConvert
    {
        /// <summary> 
        /// 1970-01-01 时间 
        /// </summary> 
        private static readonly DateTime DateTime19700101 = new DateTime(1970, 1, 1);

        /// <summary> 
        /// 根据 Unix 时间戳返回时间(毫秒) 
        /// </summary> 
        /// <param name="timestamp"></param> 
        /// <returns></returns> 
        public static DateTime GetUnixTimeStamp(long timestamp)
        {
            DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(DateTime19700101);
            return dt.AddMilliseconds(timestamp);
        }

        public static string JsonDateFormat(string timeString)
        {
            try
            {
                var date = long.Parse(timeString.Replace("/Date(", "").Replace(")/", ""));
                var time = GetUnixTimeStamp(date);
                var month = time.Month < 10 ? "0" + time.Month.ToString() : time.Month.ToString();
                var day = time.Day < 10 ? "0" + time.Day.ToString() : time.Day.ToString();
                var hours = time.Hour;
                var miniutes = time.Minute;
                var seconds = time.Second;
                var milliseconds = time.Millisecond;

                string returnHours = string.Empty, returnMinutes = string.Empty;
                if (hours < 10)
                {
                    returnHours = "0" + hours.ToString();
                }
                else
                {
                    returnHours = hours.ToString();
                }
                if (miniutes < 10)
                {
                    returnMinutes = "0" + miniutes.ToString();
                }
                else
                {
                    returnHours = miniutes.ToString();
                }

                return time.Year.ToString() + "-" + month.ToString() + "-" + day.ToString() + " " + returnHours + ":" + returnMinutes;
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
    }
}

