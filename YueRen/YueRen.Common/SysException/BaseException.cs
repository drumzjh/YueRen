using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.SysException
{
    /// <summary>
    /// 异常基类，继承ApplicationException
    /// </summary>
    public class BaseException : ApplicationException
    {

        #region "私有变量"
        /// <summary>
        /// 服务器IP地址，多个IP地址以;分割
        /// </summary>
        public string _serverIP;
        /// <summary>
        /// 服务器机器名
        /// </summary>
        public string _serverName;
        /// <summary>
        /// 异常产生时间
        /// </summary>
        public DateTime _dateTime;
        /// <summary>
        /// 异常应用域名
        /// </summary>
        private string _appDomainName;
        /// <summary>
        /// 内部异常
        /// </summary>
        private Exception _innerException;

        private int _code = Consts.SysExcepton;
        #endregion

        #region "构造函数"
        public BaseException() { }

        public BaseException(string message)
            : base(message)
        {

        }

        public BaseException(int code, string message)
            : base(message)
        {
            this.Code = code;
        }

        public BaseException(Exception inner, int code, string message)
            : base(message)
        {
            this.Code = code;
            _innerException = inner;
        }

        public BaseException(string message, Exception inner)
            : base(message, inner)
        {
            _dateTime = DateTime.Now;
            _serverIP = SystemUtility.ServerIP();
            _serverName = SystemUtility.ServerName();
            _appDomainName = SystemUtility.AppDomainName();
            _innerException = inner;

            StringBuilder sb = new StringBuilder();
            sb.Append(this.Message).Append(Environment.NewLine);
            sb.Append(this.StackTrace).Append(Environment.NewLine);
            if (inner != null)
            {
                sb.Append(inner.Message).Append(Environment.NewLine);
                sb.Append(inner.TargetSite).Append(Environment.NewLine);
                sb.Append(inner.StackTrace).Append(Environment.NewLine);

            }

            Console.WriteLine(sb.ToString());
            System.Diagnostics.Debug.WriteLine(sb.ToString());
        }

        protected BaseException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context)
        {


        }



        #endregion

        /// <summary>
        /// 编码
        /// </summary>
        public int Code
        {
            get { return _code; }
            set { this._code = value; }
        }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public String ServerIP
        {
            get
            {
                return _serverIP;
            }
            set
            {
                _serverIP = value;
            }
        }

        /// <summary>
        /// 服务器名
        /// </summary>
        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
            }
        }

        /// <summary>
        /// 异常产生时间
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
        }

        /// <summary>
        /// 异常所在应用域
        /// </summary>
        public String AppDomainName
        {
            get
            {
                return _appDomainName;
            }
        }

        ///// <summary>
        ///// 错误信息
        ///// </summary>
        //public override string Message
        //{
        //    get
        //    {
        //        return ToString();
        //    }
        //}

        /// <summary>
        /// 错误的详细信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("DateTime:").Append(DateTime).Append(Environment.NewLine);
            stringBuilder.Append("ServerIP:").Append(ServerIP).Append(Environment.NewLine);
            stringBuilder.Append("ServerName:").Append(ServerName).Append(Environment.NewLine);
            stringBuilder.Append("AppDomainName:").Append(AppDomainName).Append(Environment.NewLine);
            stringBuilder.Append("Code:").Append(Code).Append(Environment.NewLine);
            stringBuilder.Append(this.Message).Append(Environment.NewLine);
            stringBuilder.Append(this.StackTrace).Append(Environment.NewLine);
            if (_innerException != null)
            {
                stringBuilder.Append("InnerException:").Append(_innerException.GetType().ToString()).Append(Environment.NewLine);
                stringBuilder.Append("InnerExceptionString:").Append(_innerException.ToString()).Append(Environment.NewLine);



                stringBuilder.Append(_innerException.Message).Append(Environment.NewLine);
                stringBuilder.Append(_innerException.TargetSite).Append(Environment.NewLine);
                stringBuilder.Append(_innerException.StackTrace).Append(Environment.NewLine);


            }

            Console.WriteLine(stringBuilder.ToString());
            System.Diagnostics.Debug.WriteLine(stringBuilder.ToString());

            return stringBuilder.ToString();
        }
    }
}

