using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YueRen.Common.Config
{
    /// <summary>
    /// 音乐风格配置
    /// </summary>
    /// <summary>
    /// Summary description for Rings.
    /// </summary>
    [Serializable]
    [XmlRoot("MusicStyles")]
    public class MusicStyleCfg
    {
        const string fileName = "MusicStyle.xml";
        private static MusicStyleCfg instance = null;

        public static MusicStyleCfg Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    object obj = new object();
                    lock (obj)
                    {
                        if (instance != null) return instance;

                        string xml = LoadXml(fileName);
                        MusicStyleCfg cfg = new MusicStyleCfg();
                        cfg.FromXml(xml);
                        instance = cfg;
                        return instance;
                    }
                }
            }
        }

        /// <summary>
        /// 根据英文名获取风格
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public MusicStyle GetConfigByEnglishNameCode(string englishName, int code)
        {
            foreach (MusicStyle ms in listRing)
            {
                if (ms.EnglishName.ToLower() == englishName.ToLower() && ms.Code == code)
                    return ms;
            }

            return null;
        }

        /// <summary>
        /// 根据中文名获取风格
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public MusicStyle GetConfigByChineseNameCode(string chineseName, int code)
        {
            foreach (MusicStyle ms in listRing)
            {
                if (ms.ChineseName.ToLower() == chineseName.ToLower() && ms.Code == code)
                    return ms;
            }

            return null;
        }

        /// <summary>
        /// 根据风格码获取风格名,List<string>中的0为英文，1为中文名
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<string> GetStyleNameByCode(int code)
        {
            foreach (MusicStyle ms in listRing)
            {
                if (ms.Code == code)
                {
                    List<string> names = new List<string>();
                    names.Add(ms.EnglishName);
                    names.Add(ms.ChineseName);
                    return names;
                }
            }

            return null;
        }


        public InstrumentType GetConfigByCode(int code)
        {
            foreach (InstrumentType cp in listRing)
            {
                if (cp.Code == code)
                    return cp;
            }

            return null;
        }

        private static string LoadXml(string fileName)
        {
            string root = System.AppDomain.CurrentDomain.BaseDirectory;
            string webPath = YueRenConfig.Instance.AppSetting.MusicStylePath;
            FileStream fs = null;
            StreamReader rs = null;
            string xml = string.Empty;
            try
            {


                fs = new FileStream(webPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                rs = new StreamReader(fs, System.Text.Encoding.UTF8);

                xml = rs.ReadToEnd();
            }

            finally
            {
                if (rs != null) rs.Close();

                if (fs != null) fs.Close();
            }

            return xml;

        }

        private ArrayList listRing;

        public MusicStyleCfg()
        {
            listRing = new ArrayList();
        }

        [XmlElement("Error")]
        public MusicStyle[] Items
        {
            get
            {
                MusicStyle[] items = new MusicStyle[listRing.Count];
                listRing.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;

                MusicStyle[] items = (MusicStyle[])value;
                listRing.Clear();

                foreach (MusicStyle item in items)
                    listRing.Add(item);
            }
        }


        public int AddItem(MusicStyle item)
        {
            return listRing.Add(item);
        }

        /// <summary>
        /// 从xml文件获取实例
        /// </summary>
        /// <param name="xml"></param>
        public void FromXml(string xml)
        {
            MusicStyleCfg rings;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);

            XmlSerializer s = new XmlSerializer(typeof(MusicStyleCfg));

            MemoryStream ms = new MemoryStream(data);

            TextReader reader = new StreamReader(ms, System.Text.Encoding.UTF8);

            rings = s.Deserialize(reader) as MusicStyleCfg;
            reader.Close();


            if (rings == null)
                throw new ApplicationException("数据格式不正确");

            this.listRing = rings.listRing;
        }

    }

    public class MusicStyle
    {
        /// <summary>
        /// 英文名
        /// </summary>
        [XmlAttribute("EnglishName")]
        public string EnglishName;

        /// <summary>
        /// 中文名
        /// </summary>
        [XmlAttribute("ChineseName")]
        public string ChineseName;

        /// <summary>
        /// 风格码
        /// </summary>
        [XmlAttribute("Code")]
        public int Code;
    }
}


