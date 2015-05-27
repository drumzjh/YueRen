using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common
{
    /// <summary>
    /// 配置类
    /// </summary>
    public static class AppConfig
    {
        #region GetConfig
        private static NameValueCollection _settings = null;
        private static NameValueCollection Settings
        {
            get { return AppConfig._settings; }
            set { AppConfig._settings = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static string GetConfig(string key, string defaultValue)
        {
            if (_settings == null)
                _settings = ConfigurationManager.AppSettings;

            string res = Settings[key];
            if (!string.IsNullOrEmpty(res))
                return res;
            else
                return defaultValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static int GetConfig(string key, int defaultValue)
        {
            if (_settings == null)
                _settings = ConfigurationManager.AppSettings;

            string res = Settings[key];
            int outRes = defaultValue;
            if (!string.IsNullOrEmpty(res) && int.TryParse(res, out outRes))
                return outRes;
            else
                return defaultValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static bool GetConfig(string key, bool defaultValue)
        {
            if (_settings == null)
                _settings = ConfigurationManager.AppSettings;

            bool res = false;
            if (bool.TryParse(Settings[key], out res))
                return res;
            else
                return defaultValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static DateTime GetConfig(string key, DateTime defaultValue)
        {
            if (_settings == null)
                _settings = ConfigurationManager.AppSettings;

            DateTime res = DateTime.Now;
            if (DateTime.TryParse(Settings[key], out res))
                return res;
            else
                return defaultValue;
        }
        #endregion

        #region 基础工具配置
        /// <summary>
        /// 日志记录等级
        /// </summary>
        public static int LogOutLevel
        {
            get { return GetConfig("New.LogOutLevel", 0); }
        }
        /// <summary>
        /// 日志记录目录
        /// </summary>
        public static string LogFilePath
        {
            get { return GetConfig("New.LogFilePath", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogInfo")); }
        }

        /// <summary>
        /// 日志记录刷新时间(毫秒)
        /// </summary>
        public static int LogFlushIntervalMSecs
        {
            get { return GetConfig("New.LogFlushIntervalMSecs", 10000); }
        }
        #endregion

        #region 具体业务配置
        /// <summary>
        /// 是否是本地测试环境
        /// </summary>
        public static bool IsLocalTest
        {
            get { return GetConfig("IsLocalTest", false); }
        }
        #endregion

        #region 通用作家后台接口服务-配置
        /// <summary>
        /// 通用作家后台接口服务-请求服务的客户端
        /// </summary>
        public static string RpcClientAppId
        {
            get { return GetConfig("RpcClientAppId", "111"); }
        }
        /// <summary>
        /// 通用作家后台接口服务-服务提供的数据格式
        /// </summary>
        public static string RpcClientFormat
        {
            get { return GetConfig("RpcClientFormat", "json"); }
        }
        /// <summary>
        /// 通用作家后台接口服务-服务地址
        /// </summary>
        public static string RpcClientServerUrl
        {
            //get { return GetConfig("RpcClientServerUrl", "http://10.241.204.99:8991/writerservice"); }
            //get { return GetConfig("RpcClientServerUrl", "http://127.0.0.1:8991/writerservice"); }
            get { return GetConfig("RpcClientServerUrl", "http://127.0.0.1:8991/writerservice"); }
        }
        #endregion
    }
}
