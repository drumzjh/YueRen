using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Log;

namespace YueRen.Common
{
    /// <summary>
    /// HttpClient
    /// </summary>
    public static class HttpClient
    {
        #region SyncGetRequest
        /// <summary>
        /// 发起一个同步 get 请求，referer:sosu.qidian.com，使用 utf8 格式解码
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri</param>
        /// <returns>response string</returns>
        public static string SyncGetRequest(int timeout, string uri)
        {
            return SyncGetRequest(timeout, uri, null/* WebProxy */, Encoding.UTF8, "sosu.qidian.com", null/* timeout call back*/);
        }

        /// <summary>
        /// 发起一个同步 get 请求，referer:sosu.qidian.com
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri</param>
        /// <param name="encoding">encoding</param>
        /// <returns>response string</returns>
        public static string SyncGetRequest(int timeout, string uri, Encoding encoding)
        {
            return SyncGetRequest(timeout, uri, null/* WebProxy */, encoding, "sosu.qidian.com", null/* timeout call back*/);
        }

        /// <summary>
        /// 发起一个同步 get 请求，异常返回空字符串
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri string</param>
        /// <param name="proxy">web proxy</param>
        /// <param name="encoding">encoding</param>
        /// <param name="referer">referer head</param>
        /// <param name="timeoutCallback">timeout call back func</param>
        /// <returns>response string</returns>
        public static string SyncGetRequest(int timeout, string uri, WebProxy proxy, Encoding encoding, string referer, Action<WebException> timeoutCallback)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                request.Method = "GET";

                if (referer.HasValue())
                    request.Referer = referer;
                request.KeepAlive = false;
                if (timeout > 0)
                    request.Timeout = timeout;

                if (proxy != null)
                    request.Proxy = proxy;

                try
                {
                    using (WebResponse resp = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(resp.GetResponseStream(), encoding))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    LogHelper.LogError("HttpClient-SyncGetRequest-1-Url:{0}-timeout:{1}-ex:{2}", uri, timeout, ex);

                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (ex.Response != null)
                        {
                            //return ((HttpWebResponse)ex.Response).StatusCode.ToString();
                            return string.Empty;
                        }
                    }
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        if (timeoutCallback != null)
                        {
                            timeoutCallback(ex);
                        }
                    }
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("HttpClient-SyncGetRequest-0-Url:{0}-timeout:{1}-ex:{2}", uri, timeout, ex);

                return string.Empty;
            }
        }

        /// <summary>
        /// 发起一个同步 get 请求，异常返回空字符串
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri string</param>
        /// <param name="proxy">web proxy</param>
        /// <param name="referer">referer head</param>
        /// <param name="timeoutCallback">timeout call back func</param>
        /// <returns>response string</returns>
        public static byte[] SyncGetRequestBytes(int timeout, string uri, WebProxy proxy, string referer, Action<WebException> timeoutCallback)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                request.Method = "GET";

                if (referer.HasValue())
                    request.Referer = referer;
                request.KeepAlive = false;
                if (timeout > 0)
                    request.Timeout = timeout;

                if (proxy != null)
                    request.Proxy = proxy;

                try
                {
                    using (WebResponse resp = request.GetResponse())
                    {
                        return resp.GetResponseStream().ReadAsBytes();
                    }
                }
                catch (WebException ex)
                {
                    LogHelper.LogError("HttpClient-SyncGetRequestBytes-1-Url:{0}-timeout:{1}-ex:{2}", uri, timeout, ex);

                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (ex.Response != null)
                        {
                            //return ((HttpWebResponse)ex.Response).StatusCode.ToString();
                            return null;
                        }
                    }
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        if (timeoutCallback != null)
                        {
                            timeoutCallback(ex);
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("HttpClient-SyncGetRequestBytes-0-Url:{0}-timeout:{1}-ex:{2}", uri, timeout, ex);

                return null;
            }
        }
        #endregion

        #region SyncPostRequest
        /// <summary>
        /// 发起一个同步 post 请求，referer:sosu.qidian.com，使用 utf8 格式解码
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri</param>
        /// <param name="data">post data</param>
        /// <returns>response string</returns>
        public static string SyncPostRequest(int timeout, string uri, string data)
        {
            return SyncPostRequest(timeout, uri, data, null/*web proxy*/, Encoding.UTF8, "sosu.qidian.com", null/* timeout call back*/);
        }
        /// <summary>
        /// 发起一个同步 post 请求，referer:sosu.qidian.com
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri</param>
        /// <param name="data">post data</param>
        /// <param name="encoding">encoding</param>
        /// <returns>response string</returns>
        public static string SyncPostRequest(int timeout, string uri, string data, Encoding encoding)
        {
            return SyncPostRequest(timeout, uri, data, null/*web proxy*/, encoding, "sosu.qidian.com", null/* timeout call back*/);
        }
        /// <summary>
        /// 发起一个同步 post 请求
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <param name="proxy"></param>
        /// <param name="encoding"></param>
        /// <param name="referer"></param>
        /// <param name="timeoutCallback"></param>
        /// <returns></returns>
        public static string SyncPostRequest(int timeout, string uri, string data, WebProxy proxy, Encoding encoding, string referer, Action<WebException> timeoutCallback)
        {
            return SyncPostRequest(timeout, uri, data, proxy, encoding, referer, timeoutCallback, false);
        }
        /// <summary>
        /// 发起一个同步 post 请求，异常返回空字符串
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri</param>
        /// <param name="data">post data</param>
        /// <param name="proxy">proxy</param>
        /// <param name="encoding">encoding</param>
        /// <param name="referer">referer</param>
        /// <param name="timeoutCallback">timeout callback</param>
        /// <returns>response string</returns>
        public static string SyncPostRequest(int timeout, string uri, string data, WebProxy proxy, Encoding encoding, string referer, Action<WebException> timeoutCallback, bool checkValidationResult)
        {
            return SyncPostRequest(timeout, uri, data, proxy, encoding, referer, timeoutCallback, null, checkValidationResult);
        }
        /// <summary>
        /// 发起一个同步 post 请求，异常返回空字符串
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri</param>
        /// <param name="data">post data</param>
        /// <param name="proxy">proxy</param>
        /// <param name="encoding">encoding</param>
        /// <param name="referer">referer</param>
        /// <param name="timeoutCallback">timeout callback</param>
        /// <returns>response string</returns>
        public static string SyncPostRequest(int timeout, string uri, string data, WebProxy proxy, Encoding encoding, string referer, Action<WebException> timeoutCallback, Action<Exception> failCallback, bool checkValidationResult)
        {
            if (data == null)
                data = string.Empty;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var bs = encoding.GetBytes(data);
            var contentType = "application/x-www-form-urlencoded;";
            var respBytes = SyncPostRequest(timeout, uri, bs, contentType, proxy, referer, timeoutCallback, failCallback, checkValidationResult);
            if (respBytes == null)
                return string.Empty;

            return respBytes.AsString(encoding);
        }
        /// <summary>
        /// 发起一个同步 post 请求，异常返回空字符串
        /// </summary>
        /// <param name="timeout">timeout 单位:毫秒, 默认值 100,000 毫秒(100 秒)</param>
        /// <param name="uri">uri</param>
        /// <param name="data">post data</param>
        /// <param name="proxy">proxy</param>
        /// <param name="encoding">encoding</param>
        /// <param name="referer">referer</param>
        /// <param name="timeoutCallback">timeout callback</param>
        /// <returns>response string</returns>
        public static byte[] SyncPostRequest(int timeout, string uri, byte[] bs, string contentType, WebProxy proxy, string referer, Action<WebException> timeoutCallback, Action<Exception> failCallback, bool checkValidationResult)
        {
#if DEBUG
            //LogHelper.LogDebug("HttpClient-SyncPostRequest-Uri:{0}-Data:{1}", uri, data);
#endif
            if (bs == null)
                bs = new byte[0];

            try
            {
                if (checkValidationResult)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;

                request.Method = "POST";
                if (referer.HasValue())
                    request.Referer = referer;

                if (contentType.IsEmpty())
                    request.ContentType = "application/x-www-form-urlencoded;";
                else
                    request.ContentType = contentType;

                request.KeepAlive = false;

                if (timeout > 0)
                    request.Timeout = timeout;

                if (proxy != null)
                {
                    request.Proxy = proxy;
                }

                request.ContentLength = bs.Length;

                using (Stream rs = request.GetRequestStream())
                {
                    rs.Write(bs, 0, bs.Length);
                }

                try
                {
                    using (WebResponse resp = request.GetResponse())
                    {
                        return resp.GetResponseStream().ReadAsBytes();
                    }
                }
                catch (WebException ex)
                {
                    LogHelper.LogError("HttpClient-SyncPostRequest-3-Url:{0}-timeout:{1}-ex:{2}", uri, timeout, ex);

                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (ex.Response != null)
                        {
                            return null;
                        }
                    }
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        if (timeoutCallback != null)
                        {
                            timeoutCallback(ex);
                        }
                        return null;
                    }

                    if (failCallback != null)
                        failCallback(ex);

                    return null;
                }
                catch (Exception ex)
                {
                    LogHelper.LogError("HttpClient-SyncPostRequest-2-Url:{0}-timeout:{1}-ex:{2}", uri, timeout, ex);

                    if (failCallback != null)
                        failCallback(ex);

                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("HttpClient-SyncPostRequest-1-Url:{0}-timeout:{1}-ex:{2}", uri, timeout, ex);

                if (failCallback != null)
                    failCallback(ex);

                return null;
            }
        }

        /// <summary>
        /// 发起一个同步 post 请求
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="dataParameters"></param>
        /// <returns></returns>
        public static string SyncPostRequest(string uri, IList<HttpParameter> dataParameters)
        {
            return SyncPostRequest(-1, uri, dataParameters, null, string.Empty, null);
        }
        /// <summary>
        /// 发起一个同步 post 请求
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="dataParameters"></param>
        /// <param name="failCallback"></param>
        /// <returns></returns>
        public static string SyncPostRequest(string uri, IList<HttpParameter> dataParameters, Action<Exception> failCallback)
        {
            return SyncPostRequest(-1, uri, dataParameters, null, string.Empty, null, failCallback);
        }
        /// <summary>
        /// 发起一个同步 post 请求，异常返回空字符串
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="uri"></param>
        /// <param name="dataParameters"></param>
        /// <param name="encoding"></param>
        /// <param name="referer"></param>
        /// <param name="timeoutCallback"></param>
        /// <returns></returns>
        public static string SyncPostRequest(int timeout, string uri, IList<HttpParameter> dataParameters, Encoding encoding, string referer, Action<WebException> timeoutCallback)
        {
            return SyncPostRequest(timeout, uri, dataParameters, encoding, referer, timeoutCallback, null);
        }
        /// <summary>
        /// 发起一个同步 post 请求，异常返回空字符串
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="uri"></param>
        /// <param name="dataParameters"></param>
        /// <param name="encoding"></param>
        /// <param name="referer"></param>
        /// <param name="timeoutCallback"></param>
        /// <param name="failCallback"></param>
        /// <returns></returns>
        public static string SyncPostRequest(int timeout, string uri, IList<HttpParameter> dataParameters, Encoding encoding, string referer, Action<WebException> timeoutCallback, Action<Exception> failCallback)
        {
            if (string.IsNullOrEmpty(uri))
                return string.Empty;

            var data = EncodeParameters(dataParameters);
            return SyncPostRequest(timeout, uri, data, null, encoding, referer, timeoutCallback, failCallback, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
        #endregion

        #region EncodeParameters
        /// <summary>
        /// 编码 URI 参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string EncodeParameters(IList<HttpParameter> parameters)
        {
            if (parameters == null || parameters.Count <= 0)
                return string.Empty;

            var querystring = new StringBuilder();
            foreach (var p in parameters)
            {
                if (querystring.Length > 1)
                    querystring.Append("&");
                querystring.AppendFormat("{0}={1}", p.Name.UrlEncode(), p.Value.UrlEncode());
            }

            return querystring.ToString();
        }
        #endregion
    }
    /// <summary>
    /// Representation of an HTTP parameter (QueryString or Form value)
    /// </summary>
    public class HttpParameter
    {
        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Value of the parameter
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[HttpParameter Name={0}, Value={1}]", Name, Value);
        }
    }
}

