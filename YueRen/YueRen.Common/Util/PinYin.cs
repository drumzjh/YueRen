using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace YueRen.Common.Util
{
    /// <summary>
    /// 拼音处理类,  chenliang03
    /// 多音字在处理:下面两点补充较完整就能解决99%的拼音问题
    /// 1. 常用的发音:常用多音字仔细看代码请在GetNormalPoly方法中返回正确的index,比如 拓 常用词最多发tuo 仅拓本,拓片 两个词为ta ,所以要在Dictionary<int, string> polyWords中配置常用的index.
    /// 2. 把不常用或者数量少的多音词组配置在Config/PolysConfig.xml中.
    /// 之后唯一意外的情况是: 前言后语相搭到多音词上去了,比如: 韦小宝藏起来了.   这一句话里面前言后语刚好搭在 宝藏一词上面就会翻译为"宝藏",但是这可以通过如下办法解决:
    /// 在Config/PolysConfig.xml中添加"藏起来(cangqilai)并且放在"宝藏" 配置一词的前面就好了.
    /// </summary>
    public class PinYin
    {
        static string polysConfig = "";

        /// <summary>
        /// 常用多音字默认发音配置: 在ChineseChar中取最常用的发音或者组词最多的发音对应的index.
        /// </summary>
        public static readonly Dictionary<int, string> polyWords = new Dictionary<int, string>
        {
            {1,"家强卜称绰单提轧纶奇系茄芥抹迫胖汤说吓纤戏盛重石"},
            {2,"辟屏剥伯匙伺囤吭校落缪刨炮折殷粘"},
            {3,"圈"},
            {5,"朴行"}
        };

        static PinYin()
        {
            if (HttpContext.Current != null)
                polysConfig = HttpContext.Current.Server.MapPath("~/Config/PolysConfig.xml");
            else
            {
                polysConfig = AppDomain.CurrentDomain.BaseDirectory + "..\\Config\\PolysConfig.xml";
            }
        }

        /// <summary>
        /// 获取中文串的全小写拼音串（可夹杂任何非中文字符),可识别多音字.
        /// </summary>
        /// <param name="chs">中文串</param>
        /// <returns>小写拼音串</returns>
        public static string GetPinyin(string chs)
        {
            return GetPinyin(chs, 1);
        }

        /// <summary>
        /// 获取中文串的全小写拼音串（可夹杂任何非中文字符),可识别多音字.
        /// </summary>
        /// <param name="chs">中文串</param>
        /// <param name="model">1 全小写 2 全大字 3 首字大写</param>
        /// <returns>拼音串</returns>
        public static string GetPinyin(string chs, int model)
        {
            chs = ReplacePolys(chs);
            int idx = 0;
            StringBuilder sb = new StringBuilder();
            TextInfo tif = Thread.CurrentThread.CurrentCulture.TextInfo;
            foreach (char s in chs)
            {
                try
                {
                    if (ChineseChar.IsValidChar(s))
                    {
                        ChineseChar chineseChar = new ChineseChar(s);
                        idx = GetNormalPoly(s);
                        string t = chineseChar.Pinyins[idx];
                        if (model == 3)
                            t = tif.ToTitleCase(t.ToLower());
                        sb.Append(t.Substring(0, t.Length - 1));
                    }
                    else sb.Append(s);
                }
                catch (Exception e)
                {
                    sb.Append(s);
                }
            }
            if (model == 1) return sb.ToString().ToLower();
            return sb.ToString();
        }

        /// <summary>
        /// 获取多音词列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetPolys()
        {
            Dictionary<string, string> polys = new Dictionary<string, string>();
            string key = "PinYinPolys";
            object o = HttpRuntime.Cache.Get(key);
            if (null != o)
                return o as Dictionary<string, string>;
            if (File.Exists(polysConfig))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(polysConfig);
                XmlNode node = doc.DocumentElement;
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("P");
                foreach (XmlNode n in nodes)
                {
                    polys.Add(n.Attributes["key"].Value, n.InnerText);
                }
                HttpRuntime.Cache.Insert(key, polys, new System.Web.Caching.CacheDependency(polysConfig));
            }
            return polys;
        }

        /// <summary>
        /// 替换多音词
        /// </summary>
        /// <param name="chs"></param>
        /// <returns></returns>
        public static string ReplacePolys(string chs)
        {
            Dictionary<string, string> polys = GetPolys();
            if (polys.Count > 0)
            {
                string[] ps = new string[polys.Count];
                polys.Keys.CopyTo(ps, 0);
                string pss = string.Join("|", ps);

                Match match = Regex.Match(chs, pss);
                if (match.Success)
                {
                    foreach (string key in polys.Keys)
                    {
                        if (chs.IndexOf(key) > -1)
                            chs = chs.Replace(key, polys[key]);
                    }
                }
            }
            return chs;
        }

        /// <summary>
        /// 多音字常用音处理
        /// </summary>
        /// <param name="c">中文字符</param>
        /// <returns>取最常用的拼音</returns>
        public static int GetNormalPoly(char c)
        {
            foreach (int idx in polyWords.Keys)
            {
                if (polyWords[idx].IndexOf(c) > -1)
                    return idx;
            }
            return 0;
        }

        /// <summary> 
        /// 汉字串转化为拼音全大写首字母,可识别多音字.
        /// </summary> 
        /// <param name="chs">汉字串</param> 
        /// <returns>首字母串</returns> 
        public static string GetFirstPinyin(string chs)
        {
            string py = GetPinyin(chs, 3);
            return Regex.Replace(py, "[a-z]*", "");
        }

    }
}


