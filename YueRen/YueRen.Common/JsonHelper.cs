using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace YueRen.Common
{
    /// <summary>
    /// JsonHelper Wrapper
    /// </summary>
    internal class JsonHelperWrapper
    {
        /// <summary>
        /// ContentEncoding
        /// </summary>
        public Encoding ContentEncoding { get; private set; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; private set; }
        /// <summary>
        /// Data
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private JsonHelperWrapper() { }
        /// <summary>
        /// LoadData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonHelperWrapper LoadData(object data)
        {
            return LoadData(data, null /* contentType */);
        }
        /// <summary>
        /// LoadData
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static JsonHelperWrapper LoadData(object data, string contentType)
        {
            return LoadData(data, contentType, null /* contentEncoding */);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        public static JsonHelperWrapper LoadData(object data, string contentType, Encoding contentEncoding)
        {
            return new JsonHelperWrapper
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }
        /// <summary>
        /// WriteJsonResult
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jsonString"></param>
        public static void WriteJsonResult(HttpContext context, string jsonString)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponse response = context.Response;
            response.ContentType = "application/json";

            if (jsonString != null)
            {
                response.Write(jsonString);
            }
        }
        /// <summary>
        /// WriteJsonResult
        /// </summary>
        /// <param name="context"></param>
        public void WriteJsonResult(HttpContext context)
        {
            WriteJsonResult(context, -1);
        }
        /// <summary>
        /// WriteJsonResult
        /// </summary>
        /// <param name="response"></param>
        public void WriteJsonResult(HttpResponse response)
        {
            WriteJsonResult(response, -1);
        }
        /// <summary>
        /// WriteJsonResult
        /// </summary>
        /// <param name="maxJsonLength">最大可被JSON序列化的字符串长度, 小于 102400 则忽略</param>
        /// <param name="context"></param>
        public void WriteJsonResult(HttpContext context, int maxJsonLength)
        {
            ThrowHelper.ThrowIfNull(context, "context");

            WriteJsonResult(context.Response, maxJsonLength);
        }
        /// <summary>
        /// WriteJsonResult
        /// </summary>
        /// <param name="maxJsonLength">最大可被JSON序列化的字符串长度, 小于 102400 则忽略</param>
        /// <param name="response"></param>
        public void WriteJsonResult(HttpResponse response, int maxJsonLength)
        {
            ThrowHelper.ThrowIfNull(response, "response");


            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (maxJsonLength > 102400)
                    serializer.MaxJsonLength = maxJsonLength;

                response.Write(serializer.Serialize(Data));
            }
        }
    }
    /// <summary>
    /// json序列化辅助
    /// </summary>
    public static class JsonHelper
    {
        #region JavaScriptSerializer 序列化/反序列化
        /// <summary>
        /// 对象 JSON 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object data)
        {
            if (data == null)
                return null;

            return (new JavaScriptSerializer()).Serialize(data);
        }
        /// <summary>
        /// JSON 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return default(T);

            return (new JavaScriptSerializer()).Deserialize<T>(input);
        }
        #endregion

        /// <summary>
        /// 输出到 HttpContext
        /// </summary>
        /// <param name="result"></param>
        /// <param name="context"></param>
        private static void WriteJsonResult(this JsonMessageResult result, HttpContext context)
        {
            JsonHelperWrapper.LoadData(result).WriteJsonResult(context);
        }
        /// <summary>
        /// 输出到 HttpResponse
        /// </summary>
        /// <param name="result"></param>
        /// <param name="context"></param>
        public static void Out(this JsonMessageResult result, HttpResponse response)
        {
            JsonHelperWrapper.LoadData(result).WriteJsonResult(response);
        }
        /// <summary>
        /// 输出到 HttpContext
        /// </summary>
        /// <param name="result"></param>
        /// <param name="context"></param>
        public static void Out(this JsonMessageResult result, HttpContext context)
        {
            WriteJsonResult(result, context);
        }
        /// <summary>
        /// 输出到 HttpContext
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        public static void Out(this HttpContext context, JsonMessageResult result)
        {
            WriteJsonResult(result, context);
        }
    }

    /// <summary>
    /// JSON返回消息实体
    /// </summary>
    public class JsonMessageResult
    {
        public JsonMessageResult()
            : this(-1, null)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public JsonMessageResult(int result, string message)
        {
            this.Result = result;
            this.Message = message ?? string.Empty;
        }
        /// <summary>
        /// 返回码
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[JsonMessageResult Result:{0}, Message:{1}]",
                                    this.Result, this.Message);
        }

        /// <summary>
        /// 创建JSON返回消息实体
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static JsonMessageResult Create(int result, string message)
        {
            return new JsonMessageResult(result, message);
        }
    }
    /// <summary>
    /// JSON返回消息实体
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class JsonMessageResult<TData> : JsonMessageResult
    {
        /// <summary>
        /// 附属数据
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[JsonMessageResult<TData> Base:{0}, Data:{1}]",
                                    base.ToString(), this.Data);
        }

        /// <summary>
        /// 创建JSON返回消息实体
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static JsonMessageResult<TDataOut> Create<TDataOut>(int result, string message, TDataOut data)
        {
            return new JsonMessageResult<TDataOut>()
            {
                Result = result,
                Message = message,
                Data = data
            };
        }
    }
}

