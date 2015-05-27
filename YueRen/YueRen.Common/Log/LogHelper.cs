using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.Log
{
    /// <summary>
    /// 新日志模块类
    /// </summary>
    /// <remarks>
    /// 在并发比较高的服务站点建议使用本类完成, 日志记录到当前服务站点 Loginfo 目录下
    /// 默认不需要任何配置, 自定义配置请查看:
    ///     Config.LogOutLevel
    ///     Config.LogFilePath
    ///     Config.LogFlushIntervalMSecs
    /// </remarks>
    public static class LogHelper
    {
        #region WriteErrLog
        /// <summary>
        /// WriteErrLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            CMLogger.GetInstance().WriteErrLog(null, null, msg, null);
        }
        /// <summary>
        /// WriteErrLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string format, params object[] para)
        {
            Error(string.Format(format, para));
        }
        /// <summary>
        /// WriteErrLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogError(string msg)
        {
            Error(msg);
        }
        /// <summary>
        /// WriteErrLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogError(string format, params object[] para)
        {
            Error(string.Format(format, para));
        }
        #endregion

        #region WriteDebugLog
        /// <summary>
        /// WriteDebugLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            CMLogger.GetInstance().WriteDebugLog(null, null, msg);
        }
        /// <summary>
        /// WriteDebugLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string format, params object[] para)
        {
            Debug(string.Format(format, para));
        }
        /// <summary>
        /// WriteDebugLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogDebug(string msg)
        {
            Debug(msg);
        }
        /// <summary>
        /// WriteDebugLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogDebug(string format, params object[] para)
        {
            Debug(string.Format(format, para));
        }
        #endregion

        #region WriteSingleLine
        /// <summary>
        /// WriteWarnLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            CMLogger.GetInstance().WriteWarnLog(null, null, msg);
        }
        /// <summary>
        /// WriteWarnLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string format, params object[] para)
        {
            Warn(string.Format(format, para));
        }
        /// <summary>
        /// WriteWarnLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogWarn(string msg)
        {
            Warn(msg);
        }
        /// <summary>
        /// WriteWarnLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogWarn(string format, params object[] para)
        {
            Warn(string.Format(format, para));
        }
        #endregion

        #region WriteInfoLog
        /// <summary>
        /// WriteInfoLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            CMLogger.GetInstance().WriteInfoLog(null, null, msg);
        }
        /// <summary>
        /// WriteInfoLog
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string format, params object[] para)
        {
            Info(string.Format(format, para));
        }
        /// <summary>
        /// WriteInfoLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogInfo(string msg)
        {
            Info(msg);
        }
        /// <summary>
        /// WriteInfoLog
        /// </summary>
        /// <param name="msg"></param>
        public static void LogInfo(string format, params object[] para)
        {
            Info(string.Format(format, para));
        }
        #endregion
    }
}
