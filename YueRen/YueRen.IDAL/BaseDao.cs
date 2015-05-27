using GotDotNet.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common;
using YueRen.Common.Config;

namespace YueRen.IDAL
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public abstract class BaseDao
    {
        /// <summary>
        /// 表示-1值
        /// </summary>
        public const int NULL_PARAM_INT = -1;
        /// <summary>
        /// 用户Double类型，表示0
        /// </summary>
        public const double NULL_PARAM_DOUBLE = 0.0;
        /// <summary>
        /// 表示空字符串
        /// </summary>
        public const string NULL_PARAM_STRING = "";

        /// <summary>
        /// 表示-1值
        /// </summary>
        public const long NULL_PARAM_LONG = -1;

        public const bool DATETIMETYPE = true;
        const string ORA_PROVIDER_NAME = "oracle";
        const string SQL_PROVIDER_NAME = "sqlserver";
        //add by zch
        const string MySql_PROVIDER_NAME = "mysql";

        /// <summary>
        /// 数据访问帮助类
        /// </summary>
        protected AdoHelper adoHelper;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public abstract string ConnectionString
        {
            get;
        }

        /// <summary>
        /// 根据BookId分区获取镜像的数据访问连接
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        protected virtual string GetBookConnectionString(string bookId)
        {
            return null;
        }

        /// <summary>
        /// 根据类型决定该类是否使用Oracle实现类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected static bool IsCfgOracleInstance(Type type)
        {
            ClassProvider cp = ClassProvidersCfg.Instance.GetConfigByName(type.Name);
            if (cp != null)
            {
                if (cp.Provider.ToUpper() == ORA_PROVIDER_NAME.ToUpper())
                {
                    return true;
                }
                else if (cp.Provider.ToUpper() == SQL_PROVIDER_NAME.ToUpper())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据ErrorCode得到数据库错误消息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual string GetDBMessage(int code)
        {
            DBMsgProvider provider = DBMsgProviderCfg.Instance.GetConfigByCode(code);
            if (provider != null)
            {
                return provider.Type + "|" + provider.SPName + "|" + provider.Message;
            }
            else
            {
                return "DB Error";
            }
        }

        /// <summary>
        /// 根据类型获取默认的数据库访问帮助类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual AdoHelper GetDefaultProvider(Type type)
        {
            if (BaseDao.IsCfgOracleInstance(type))
            {
                return this.GetOracleProvider();
            }
            else
            {
                return this.GetSqlProvider();
            }
        }

        /// <summary>
        /// 获取Sql Server数据访问帮助类
        /// </summary>
        /// <returns></returns>
        protected AdoHelper GetSqlProvider()
        {
            //if (CmfuConfig.Instance.AppSetting.LocalDacCache)
            //    return GetProxySqlProvider();
            return AdoHelper.CreateHelper(DbProvideType.SqlServer);
        }

        /// <summary>
        ///  获取oracle数据访问帮助类
        /// </summary>
        /// <returns></returns>
        protected AdoHelper GetOracleProvider()
        {
            //if (CmfuConfig.Instance.AppSetting.LocalDacCache)
            //    return GetProxyOracleProvider();
            return AdoHelper.CreateHelper(DbProvideType.Oracle);
        }
        /// <summary>
        ///  获取MySql数据访问类
        /// add by zch
        /// </summary>
        /// <returns></returns>
        protected AdoHelper GetMySqlProvider()
        {
            return AdoHelper.CreateHelper(DbProvideType.MySql);
        }

        //private AdoHelper GetProxyOracleProvider()
        //{
        //    return Snda.Qidian.DataAccess.Proxy.DACProxy.CreateHelper(DbProvideType.Oracle);
        //}

        //private AdoHelper GetProxySqlProvider()
        //{
        //    return Snda.Qidian.DataAccess.Proxy.DACProxy.CreateHelper(DbProvideType.SqlServer);
        //}
        /// <summary>
        /// 
        /// </summary>
        protected BaseDao() { }

        /// <summary>
        /// 消除逗号分割的开头和结果的逗号
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        protected string RemoveDel(string strList)
        {
            if (string.IsNullOrEmpty(strList))
                return strList;

            if (strList.EndsWith(","))
                strList = strList.Remove(strList.Length - 1);

            if (strList.StartsWith(","))
                return strList.Remove(0, 1);

            return strList;
        }
    }
}

