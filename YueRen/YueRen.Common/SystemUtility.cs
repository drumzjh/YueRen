using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using YueRen.Common.Log;

namespace YueRen.Common
{
    /// <summary>
    /// 系统工具类
    /// </summary>
    public static class SystemUtility
    {
        /// <summary>
        /// 一个全局随机器
        /// </summary>
        public readonly static Random SysRandom = new Random();

        public static bool IsValidEmailAddr(string emailAddr)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(emailAddr);
        }

        /// <summary>
        /// 本地计算机名
        /// </summary>
        /// <returns></returns>
        public static string ServerName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        /// 本地计算机IP4地址
        /// </summary>
        /// <returns></returns>
        public static string ServerIPV4()
        {
            string machineIP = String.Empty;
            IPAddress[] ips = Dns.GetHostAddresses(ServerName());
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    machineIP = ip.ToString();
                    break;
                }
            }

            if (string.IsNullOrEmpty(machineIP))
            {
                machineIP = "127.0.0.1";
            }

            return machineIP;
        }

        /// <summary>
        /// 本地计算机IP地址，如果是多个地址则用";"分割
        /// </summary>
        /// <returns></returns>
        public static string ServerIP()
        {
            string machineIP = String.Empty;
            IPAddress[] ips = Dns.GetHostAddresses(ServerName());
            foreach (IPAddress ip in ips)
            {
                machineIP += ip.ToString() + ";";
            }
            if (machineIP.EndsWith(";"))
            {
                machineIP = machineIP.Substring(0, machineIP.Length - 1);
            }
            return machineIP;
        }

        /// <summary>
        /// 应用域的名称
        /// </summary>
        /// <returns></returns>
        public static string AppDomainName()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        /// <param name="httpRequest">当前用户请求</param>
        /// <returns></returns>
        public static string ClientIP(HttpRequest httpRequest)
        {
            return UserTrueIp;
        }
        /*   sb.Append( "HTTP_CLIENT_IP:" + HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] );
                        sb.Append( "HTTP_X_FORWARDED_FOR:" + HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] );*/

        public static string SquidForwardIp
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    //string netSCalerIp=  HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                    //if (!string.IsNullOrEmpty(netSCalerIp))
                    //    return netSCalerIp;

                    string forwordList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(forwordList))
                        return string.Empty;

                    string[] ipList = forwordList.Split(new char[] { ',' });
                    if (ipList.Length > 0)
                    {
                        return GetPublicIP(ipList);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        private static string GetPublicIP(string[] ipList)
        {
            if (ipList == null || ipList.Length <= 0)
                return string.Empty;

            Regex rx = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            string ip = string.Empty;
            string[] ipArray;
            for (int i = 0; i < ipList.Length; i++)
            {
                if (string.IsNullOrEmpty(ipList[i]))
                    continue;

                ip = ipList[i].Trim();
                if (rx.IsMatch(ip) && !ip.StartsWith("192.168.") && !ip.StartsWith("255.") && !ip.StartsWith("127.") && !ip.StartsWith("10."))
                {
                    if (ip.StartsWith("172."))
                    {
                        ipArray = ip.Split('.');
                        if (ipArray.Length > 1 && int.Parse(ipArray[1]) >= 16 && int.Parse(ipArray[1]) <= 31)
                        {
                            continue;
                        }
                    }
                    return ip;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取X_FORWARDED_FOR IP
        /// </summary>
        public static string SquidForwardIp2
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    string forwordList = HttpContext.Current.Request.ServerVariables["X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(forwordList))
                        return string.Empty;

                    string[] ipList = forwordList.Split(new char[] { ',' });
                    if (ipList.Length >= 2 && !ipList[0].Equals("unknown"))
                    {
                        return ipList[0];
                    }
                    else if (ipList.Length >= 2 && !ipList[1].Equals("unknown"))
                    {
                        return ipList[1];
                    }
                    else if (ipList.Length == 1 && !ipList[0].Equals("unknown"))
                    {
                        return ipList[0];
                    }
                    else if (!forwordList.StartsWith("10.") && !forwordList.StartsWith("unknown"))
                    {
                        return forwordList;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 获取HttpCdnSrcIp
        /// </summary>
        public static string HttpCdnSrcIp
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    string cdnSrcIp = HttpContext.Current.Request.ServerVariables["HTTP_CDN_SRC_IP"];
                    if (string.IsNullOrEmpty(cdnSrcIp))
                        return string.Empty;
                    if (cdnSrcIp.Contains(",") || cdnSrcIp.Contains("，"))
                    {
                        cdnSrcIp = cdnSrcIp.Replace(",", "").Replace("，", "");
                    }
                    return cdnSrcIp.Trim();
                }
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获取用户真实的ip
        /// </summary>
        public static string UserTrueIp
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    if (!string.IsNullOrEmpty(SquidForwardIp)) return SquidForwardIp.Trim();

                    string trueIp = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];

                    if (string.IsNullOrEmpty(trueIp))
                        trueIp = HttpContext.Current.Request.UserHostAddress;

                    if (trueIp.Contains(",") || trueIp.Contains("，"))
                    {
                        trueIp = trueIp.Replace(",", "").Replace("，", "");
                    }
                    return trueIp.Trim();
                }
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获取用户HttpClientIP
        /// </summary>
        public static string HttpClientIP
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    string trueIp = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                    if (string.IsNullOrEmpty(trueIp))
                        trueIp = HttpContext.Current.Request.UserHostAddress;
                    if (trueIp.Contains(",") || trueIp.Contains("，"))
                    {
                        trueIp = trueIp.Replace(",", "").Replace("，", "");
                    }
                    return trueIp.Trim();
                }
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public static string UserIp
        {
            get
            {
                return UserTrueIp;
            }
        }

        /// <summary>
        /// 当前请求用户
        /// </summary>
        public static string CurrentUserId
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    return CurrentUser(HttpContext.Current);
                }
                else
                    return string.Empty;
            }
        }


        /// <summary>
        /// 当前用户请求的Url地址
        /// </summary>
        public static string RequestUrl
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null
                    && HttpContext.Current.Request.Url != null)
                {
                    return HttpContext.Current.Request.Url.PathAndQuery;
                    // return HttpContext.Current.Request.Url.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string DeriveFileName(string Url)
        {

            if (Url == null)
                throw new ArgumentNullException("fileName", "文件路径名称为空");

            if (Url.Length == 0 || Url.IndexOf(".") == -1) return string.Empty;


            Url = Url.Replace(@"/", @"\");

            if (Url.IndexOf("\\") != -1)
            {
                string[] filePieces = Url.Split('\\');
                string path = filePieces[filePieces.Length - 1];

                int len = path.IndexOf(".aspx");

                if (len > 0)
                {

                    return path.Substring(0, len) + ".aspx";
                }
                else
                {
                    return path;
                }

            }
            else
            {
                return Url;
            }
        }

        /// <summary>
        /// 当前请求用户
        /// </summary>
        /// <param name="httpContext">当前用户山下文</param>
        /// <returns>匿名用户返回""</returns>
        public static string CurrentUser(HttpContext httpContext)
        {
            if (httpContext != null)
            {
                IPrincipal curPrincipal = httpContext.User;
                if (curPrincipal != null)
                {
                    IIdentity curIdentity = curPrincipal.Identity;
                    if (curIdentity != null)
                    {
                        return curIdentity.Name;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 格式化日志信息
        /// </summary>
        /// <param name="message">原始信息</param>
        /// <returns></returns>
        public static string FormatLogMessage(string message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("DateTime:").Append(DateTime.Now).Append(Environment.NewLine);
            stringBuilder.Append("ServerIP:").Append(ServerIP()).Append(Environment.NewLine);
            stringBuilder.Append("ServerName:").Append(ServerName()).Append(Environment.NewLine);
            stringBuilder.Append("ClientIP:").Append(UserIp).Append(Environment.NewLine);
            stringBuilder.Append("CurrentUser:").Append(CurrentUserId).Append(Environment.NewLine);
            stringBuilder.Append("Url:").Append(RequestUrl).Append(Environment.NewLine);
            stringBuilder.Append("PreferUrl:").Append(PreferUrl).Append(Environment.NewLine);
            stringBuilder.Append("UserAgent:").Append(UserAgent).Append(Environment.NewLine);


            stringBuilder.Append("Message:").Append(message).Append(Environment.NewLine);
            return stringBuilder.ToString();
        }

        public static string PreferUrl
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    if (HttpContext.Current.Request.UrlReferrer != null)
                        return HttpContext.Current.Request.UrlReferrer.ToString();
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }

        public static string UserAgent
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    return HttpContext.Current.Request.UserAgent;
                }
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string[] SplitString(string source, string split)
        {
            return Strings.Split(source, split, -1, CompareMethod.Text);
        }

        public static string UrlEncode(string text)
        {
            return HttpUtility.UrlEncode(text);
        }

        /// <summary>
        /// 返回md5加密串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetMd5Data(string data)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;

            System.Security.Cryptography.MD5CryptoServiceProvider csp = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] result = csp.ComputeHash(encoding.GetBytes(data));
            System.Text.StringBuilder EnText = new System.Text.StringBuilder();
            foreach (byte Byte in result)
            {
                EnText.AppendFormat("{0:X2}", Byte);
            }
            return EnText.ToString();
        }


        /// <summary>
        /// 将字符串转换为 Unicode 外观的字符串
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns></returns>
        /// <remarks>
        /// 例如'{}X,xiao[(&#38;*^%$#@!)]'`~"开心 起点　《》（）'->'{}X,xiao[(&#38;*^%$#@!)]\u0027`~\u0022\u5f00\u5fc3\u0020\u8d77\u70b9\u3000\u300a\u300b\uff08\uff09'
        /// 本方法不单单转换汉字, 所有字符串都可以. 
        /// 可以用来往客户端输出JS字符串, 而不被'斜杠/空格/换行/引号'等的困扰
        /// </remarks>
        public static string GBToUnicode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder builder = new StringBuilder(text.Length * 6);

            char[] charArray = text.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                char c = charArray[i];
                if (c == '\b')
                    builder.Append("\\b");
                else if (c == '\f')
                    builder.Append("\\f");
                else if (c == '\n')
                    builder.Append("\\n");
                else if (c == '\r')
                    builder.Append("\\r");
                else if (c == '\t')
                    builder.Append("\\t");
                else
                {
                    int codepoint = Convert.ToInt32(c);
                    if (c != '\'' && c != '"' && c != '\\' && (codepoint >= 33 && codepoint <= 126))
                        builder.Append(c);
                    else
                        builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                }
            }
            return builder.ToString();
        }


        /// <summary>
        /// ip 4转换的4位char组合  （其中的  124 = |    会替换成  256 = Ā  来实现）
        /// </summary>
        /// <param name="ipString"></param>
        /// <returns></returns>
        public static string IpV4ToCharString(string ipString)
        {
            StringBuilder charString = new StringBuilder();
            if (!string.IsNullOrEmpty(ipString))
            {
                IPAddress tip;

                if (IPAddress.TryParse(ipString, out tip))
                {
                    string[] ipc = ipString.Split('.');
                    if (ipc.Length == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            charString.Append(char.ConvertFromUtf32(int.Parse(ipc[i])));
                        }
                    }
                }
            }

            return charString.ToString().Replace('|', 'Ā');
        }

        /// <summary>
        /// 4位char组合转换成ip 4     （其中的  124 = |    会替换成  256 = Ā  来实现）  
        /// </summary>
        /// <param name="charString"></param>
        /// <returns></returns>
        public static string CharStringToIpV4(string charString)
        {
            StringBuilder ipString = new StringBuilder();
            if (!string.IsNullOrEmpty(charString))
            {
                if (charString.Length == 4)
                {
                    charString = charString.Replace('Ā', '|');
                    for (int i = 0; i < 4; i++)
                    {
                        int ipnum = char.ConvertToUtf32(charString[i].ToString(), 0);

                        if (ipnum > 255 || ipnum < 0)
                            return string.Empty;

                        ipString.Append(".").Append(ipnum.ToString());
                    }
                }
            }
            return (ipString.Length > 0) ? ipString.Remove(0, 1).ToString() : ipString.ToString();
        }


        /// <summary>
        /// 生成一个ticketid  表示唯一性
        /// </summary>
        /// <returns></returns>
        public static string GetTicketId()
        {
            string ticketid = CreateTicketId();

            //生成失败重试两次
            if (string.IsNullOrEmpty(ticketid))
            {
                ticketid = CreateTicketId();

                if (string.IsNullOrEmpty(ticketid))
                    ticketid = CreateTicketId();
            }

            return ticketid;

        }

        /// <summary>
        /// 生成一个ticketid  表示唯一性
        /// </summary>
        /// <returns></returns>
        private static string CreateTicketId()
        {
            string ticketid = string.Empty;
            try
            {
                string randStr = string.Empty;

                byte[] randb = new byte[4];
                RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

                rand.GetBytes(randb);
                int rad = BitConverter.ToInt32(randb, 0);

                rad = rad % (999999 - 100000 + 1);
                if (rad < 0)
                    rad = -rad;

                randStr = (rad + 100000).ToString();

                string ticks = DateTime.Now.Ticks.ToString();

                string pid = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

                string threadID = AppDomain.GetCurrentThreadId().ToString();

                IPHostEntry IpEntry = Dns.GetHostEntry(System.Net.Dns.GetHostName());

                string myip = IpEntry.AddressList[0].ToString().Split(new char[] { '.' })[3].ToString();

                ticketid = string.Concat(ticks, pid, threadID, randStr, myip);

            }
            catch (Exception ex)
            {
                LogHelper.Error("生成ticketid失败，ex={0}", ex.Message);
                ex = null;
                ticketid = string.Empty;
            }
            return ticketid;
        }


        /// <summary>
        /// 是否移动设备 服务器端判断   （静态站点cdn  不能用这个判断，要用js）
        /// </summary>
        /// <returns></returns>
        public static bool IsMobileDevice()
        {
            bool isMobile = false;

            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {

                //FIRST TRY BUILT IN ASP.NT CHECK
                if (HttpContext.Current.Request.Browser.IsMobileDevice)
                {
                    isMobile = true;
                }

                if (!isMobile)
                {
                    //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
                    if (HttpContext.Current.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
                    {
                        isMobile = true;
                    }
                }

                if (!isMobile)
                {
                    //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
                    string httpAccept = HttpContext.Current.Request.ServerVariables["HTTP_ACCEPT"];
                    httpAccept = (string.IsNullOrEmpty(httpAccept) ? "" : httpAccept);
                    if (!string.IsNullOrEmpty(httpAccept) &&
                        httpAccept.IndexOf("wap", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        isMobile = true;
                    }
                }

                if (!isMobile)
                {
                    string useragent = HttpContext.Current.Request.UserAgent;
                    string ydbs = "ipad|iphone os|android|windows mobile|windows phone|symbian|hpwos|blackberry|nokiabrowser|nokia|mqqbrowser|htc_sensation";
                    string[] ydbss = ydbs.Split('|');
                    foreach (string bs in ydbss)
                    {
                        //判断手持设备
                        if (useragent.IndexOf(bs, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            isMobile = true;
                            break;
                        }
                    }
                }
            }

            return isMobile;
        }


        #region 过滤盛大通行证

        /// <summary>
        /// 过滤盛大通行证账号
        /// </summary>
        /// <param name="passport">盛大通行证账号</param>
        /// <returns></returns>
        public static string CheckSNDAPassport(string passport)
        {

            if (string.IsNullOrEmpty(passport))
                return passport;

            try
            {
                long outname;
                if (long.TryParse(passport, out outname))  //数字直接输出
                    return passport;

                if (passport.IndexOf('.') > -1)
                {
                    string preName = passport.Substring(0, passport.IndexOf('.') - 1); //前缀
                    string lastName = passport.Substring(passport.IndexOf('.'));  //后缀
                    if (preName.Length <= 3)
                    {
                        return preName + "****" + lastName;
                    }
                    else
                    {
                        return preName.Substring(0, 3) + "****" + lastName;
                    }
                }
                else
                {
                    if (passport.Length <= 3)
                    {
                        return passport + "****";
                    }
                    else
                    {
                        return passport.Substring(0, 3) + "****" + passport.Substring(passport.Length - 1);
                    }
                }
            }
            catch
            {
                return passport;
            }
        }

        #endregion


        #region 判断新版女生网

        /// <summary>
        /// 女生网作品大类
        /// </summary>
        private static int[] MMCategoryIds = { 41, 71, 72, 73, 74, 75, 76, 77, 78, 79 };
        /// <summary>
        /// 起点作品大类,不包括女生网
        /// </summary>
        private static int[] QidianCategoryIds = { 1, 21, 2, 22, 4, 15, 5, 6, 7, 8, 9, 10, 12, 14, 30, 31, 51, 60, 61, 63, 65, 67, 98, 99 };
        /// <summary>
        /// 判断分类编号是否是女生网类型
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <returns></returns>
        public static bool IsMMCategory(int categoryId)
        {
            return (Array.IndexOf(MMCategoryIds, categoryId) != -1);
        }

        /// <summary>
        /// 判断整个起点分类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool IsQidianCategory(int categoryId)
        {
            if ((Array.IndexOf(MMCategoryIds, categoryId) != -1)) return true;
            return (Array.IndexOf(QidianCategoryIds, categoryId) != -1);
        }
        #endregion

        /// <summary>
        /// 获取url的host
        /// </summary>
        static Regex regUrlHostCheck = new Regex(@"http(s)?://(?<domain>[^:/]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// 判断是否是起点url (qidian  qdmm  qdwenxue cmfu qidianmm)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsQidianHostUrl(string url)
        {
            string urldomain = GetUrlHost(url);
            if (!string.IsNullOrEmpty(urldomain)
                && (urldomain.EndsWith(".qidian.com", StringComparison.OrdinalIgnoreCase) ||
                urldomain.EndsWith(".qdmm.com", StringComparison.OrdinalIgnoreCase) ||
                urldomain.EndsWith(".qdwenxue.com", StringComparison.OrdinalIgnoreCase) ||
                urldomain.EndsWith(".cmfu.com", StringComparison.OrdinalIgnoreCase) ||
                urldomain.EndsWith(".qidianmm.com", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取url host
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlHost(string url)
        {
            string host = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                Match matchurl = regUrlHostCheck.Match(url);
                if (matchurl != null && matchurl.Success)
                {
                    host = matchurl.Groups["domain"].Value;
                }
            }

            return host;

        }

        #region 圆梦众筹API接口
        /// <summary>
        /// 返回json格式
        /// </summary>
        /// <param name="retcode">错误码</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static string DreamFormatJson(int retcode, string msg)
        {
            string jsonFormat = "{{\"HTTP_RETURN_CODE\":{0},\"RETURN_MESSAGE\":\"{1}\"}}";
            string json = string.Format(jsonFormat, retcode, msg);
            return json;
        }

        ///// <summary>
        ///// 获取签名的公共方法
        ///// </summary>
        ///// <param name="sigFormat">格式请按照 param1&param2&param3 传入</param>
        ///// <param name="parameters"></param>
        ///// <returns>返回MD5签名字符串，如果传入的参数不一致，返回空字符串</returns>
        //public static string GetSignatureFormat(string sigFormat, params object[] args)
        //{
        //    string result = "";
        //    string signature = "";
        //    if (!string.IsNullOrEmpty(sigFormat) && args != null & args.Length > 0)
        //    {
        //        string[] array = sigFormat.Split('&');
        //        int length = array.Length;
        //        //format的分割长度需要与args的长度一样
        //        if (length != args.Length)
        //            return "";
        //        for (int i = 0; i < length; i++)
        //        {
        //            string key = array[i];
        //            string value = args[i].ToString();
        //            if (value.Length > 10)
        //                value = value.Substring(0, 10);
        //            result += key + "=" + value + "&"; ;
        //        }
        //        result += CmfuConfig.Instance.AppSetting.DreamKey;
        //        LogHelper.Info("圆梦众筹签名串：" + result);
        //        signature = GetMd5Data(result).ToLower();
        //    }

        //    return signature;
        //}

        /// <summary>
        /// HTTP 输出返回值并结束
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static void ResponseWriteEnd(string content)
        {
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Write(content);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        #endregion

        /// <summary>
        /// 获取签名的公共方法
        /// </summary>
        /// <param name="sigFormat">格式请按照 param1&param2&param3 传入</param>
        /// <param name="parameters"></param>
        /// <returns>返回MD5签名字符串，如果传入的参数不一致，返回空字符串</returns>
        public static string GetSignatureFormatEx(string signkey, string sigFormat, params object[] args)
        {
            string result = "";
            string signature = "";
            if (!string.IsNullOrEmpty(sigFormat) && args != null & args.Length > 0)
            {
                string[] array = sigFormat.Split('&');
                int length = array.Length;
                //format的分割长度需要与args的长度一样
                if (length != args.Length)
                    return "";
                for (int i = 0; i < length; i++)
                {
                    string key = array[i];
                    string value = args[i].ToString();
                    if (value.Length > 10)
                        value = value.Substring(0, 10);
                    result += key + "=" + value + "&"; ;
                }
                result += signkey;
                //LogHelper.Info("签名串：" + result);
                signature = GetMd5Data(result).ToLower();
                //LogHelper.Info("加密签名串：" + signature);
            }

            return signature;
        }

        #region GetRequestParam
        /// <summary>
        /// 获取 TResult 类型的参数值Request[key]
        /// </summary>
        /// <param name="key">参数关键字</param>
        /// <param name="val">获取失败时返回的默认值</param>
        /// <returns>TResult 类型的参数值，如果获取失败返回默认值</returns>
        public static TResult GetRequestParam<TResult>(string key, TResult val, Func<string, TResult> parseAction)
        {
            ThrowHelper.ThrowIfNull(HttpContext.Current, "HttpContext.Current");
            ThrowHelper.ThrowIfNull(parseAction, "parseAction");

            if (key.IsEmpty())
                return val;

            return parseAction(HttpContext.Current.Request[key]);
        }
        /// <summary>
        /// 获取 TResult 类型的参数值列表, 逗号分隔, Request[key]
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="key"></param>
        /// <param name="val">获取失败时返回的默认值</param>
        /// <returns></returns>
        public static TResult[] GetRequestParams<TResult>(IHttpHandler handler, string key, string splitChar, TResult val, Func<string, TResult> parseAction)
        {
            ThrowHelper.ThrowIfNull(HttpContext.Current, "HttpContext.Current");
            ThrowHelper.ThrowIfNull(parseAction, "parseAction");

            if (key.IsEmpty())
                return null;

            var s = HttpContext.Current.Request[key];
            if (s.IsEmpty())
                return null;

            var ss = s.Split(new string[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
            if (ss == null || ss.Length <= 0)
                return null;

            var outList = new List<TResult>(ss.Length);
            foreach (var cs in ss)
                outList.Add(parseAction(cs));

            return outList.ToArray();
        }
        /// <summary>
        /// 获取 int 类型的参数值Request[key]
        /// </summary>
        /// <param name="key">参数关键字</param>
        /// <param name="val">获取失败时返回的默认值</param>
        /// <returns>int 类型的参数值，如果获取失败返回默认值</returns>
        public static int GetIntRequestParam(string key, int val)
        {
            return GetRequestParam(key, val, s =>
            {
                var rel = val;
                if (!int.TryParse(s, out rel))
                    rel = val;

                return rel;
            });
        }
        /// <summary>
        /// 获取 string 类型的参数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">获取失败时返回的默认值</param>
        /// <returns></returns>
        public static string GetStringRequestParam(string key, string val)
        {
            return GetRequestParam(key, val, s =>
            {
                if (s.IsEmpty())
                    return val;

                return s;
            });
        }
        #endregion

        #region 时间戳获取
        /// <summary>
        /// 1970-01-01 时间
        /// </summary>
        public static readonly DateTime DateTime19700101 = new DateTime(1970, 1, 1);
        /// <summary>
        /// 获取 Unix 时间戳(毫秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetUnixTimeStamp(DateTime dt)
        {
            TimeSpan ts = new TimeSpan(dt.ToUniversalTime().Ticks - DateTime19700101.Ticks);
            return (long)ts.TotalMilliseconds;
        }
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
        /// <summary>
        /// timestamp   (1970年以来的秒数)
        /// </summary>
        /// <returns></returns>
        public static long GetNowTimestamp()
        {
            return GetNowTimestamp(DateTime.Now);
        }
        /// <summary>
        /// timestamp   (1970年以来的秒数)
        /// </summary>
        /// <returns></returns>
        public static long GetNowTimestamp(DateTime dtTime)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(DateTime19700101);
            var toNow = dtTime.Subtract(dtStart);
            return (long)toNow.TotalSeconds;
        }
        #endregion

        /// <summary>
        /// 根据书的Type信息获得当前书应该使用的交易 AreaId
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <remarks>
        ///  AreaId:  主站 1, 女频 6, 文学网 20
        /// </remarks>
        public static int GetCurrentAreaId()
        {
            // var cAreaId = CmfuConfig.Instance.AppSetting.BillingConsumeAreaId;
            // 主站 1, 女频 6, 文学网 20
            int cAreaId = 30;

            if (HttpContext.Current != null)
            {
                var host = HttpContext.Current.Request.Url.Host.ToLower();
                if (host.IndexOf("3g.if") > -1)
                {
                    cAreaId = 30; //android 客户端
                }
                else if (host.IndexOf("iosm") > -1)
                {
                    cAreaId = 33; //iphone客户端
                }
                else if (host.IndexOf("2.if") > -1)
                {
                    cAreaId = 36; //wp客户端
                }
                else if (host.IndexOf("3.if") > -1)
                {
                    cAreaId = 37; //win8客户端
                }
                else if (host.IndexOf("4.if") > -1)
                {
                    cAreaId = 42; //起点读书Android单行本
                }
                else if (host.IndexOf("5.if") > -1)
                {
                    cAreaId = 44;//iOS端单本消费
                }

                //--AreaId=51 萌端
                //--判断是否iPhone客户端 AppStore版本
                var pAreaId = GetIntRequestParam("AreaId", 0);//AreaId=51 萌端
                if (pAreaId > 0)
                {
                    cAreaId = pAreaId;

                    //LogHelper.LogDebug("Util-SystemUtility.GetCurrentAreaId-{0}", pAreaId);
                }
            }

            // add by chenzixia 2012.2.1
            //if (cAreaId == 30)
            //{
            //    string min = DateTime.Now.Minute.ToString();
            //    int lastnum = int.Parse(min.Length == 1 ? min[0].ToString() : min[1].ToString());
            //    if (lastnum == 0 || lastnum == 5 || lastnum == 9)
            //    {
            //        cAreaId = 34;
            //    }
            //}

            return cAreaId;
        }

        /// <summary>
        /// 判断是否汉字
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsHanZi(string ch)
        {
            byte[] byte_len = System.Text.Encoding.Default.GetBytes(ch);
            if (byte_len.Length == 2) { return true; }
            return false;
        }

        /// <summary>
        /// 获取第一个汉字的首字母，只能输入汉字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "J";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }
    }
}

